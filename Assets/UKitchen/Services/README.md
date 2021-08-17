## Services

## Requirements
* [Async Await Support](https://assetstore.unity.com/packages/tools/integration/async-await-support-101056)   //   [Blog](http://www.stevevermeulen.com/index.php/2017/09/using-async-await-in-unity3d-2017/)

Create your service response class
```c#
public class DogImageData : AbsServiceResponse
{
    public string message;
    public string status;
}
```

Create our service response signal class
```c#
public class DogImageResponseSignal : AbsServiceSignal<DogImageData>
{
    public DogImageResponseSignal(DogImageData response) : base(response)
    {
    }
}
```

Create our service class
```c#
public class RandomDogService : AbsServiceManager
{
    [Inject] private SignalBus _signal;
    
    public async void GetRandomDogImage()
    {
        var url = "https://dog.ceo/api/breeds/image/random";

        var sentTask = ResponseWithJson<DogImageData>(url, "GetRandomDogImage");

        await sentTask;
        
        _signal.Fire(new DogImageResponseSignal(sentTask.Result));
    }
}
```

Finally, binding our service and signal
```c#
public class AppInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<RandomDogService>().AsSingle();

        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<DogImageResponseSignal>();
    }
}
```

For example, to call and listen to our service from the menu:
```c#
public class MyFirstMenu : Menu<MenuName, MyFirstMenuArgs, MyFirstMenu>
{
    [Inject] private readonly SignalBus _signal;
    [Inject] private RandomDogService _service;
    [SerializeField] private Text _serviceResultTxt;

    public void OnClick_RandomDogBtn()
    {
        _service.GetRandomDogImage();
    }
    
    protected override void OnShowBefore<TMenuArgs>(TMenuArgs e)
    {
        _signal.TryUnsubscribe<DogImageResponseSignal>(DogImageResult);
        _signal.Subscribe<DogImageResponseSignal>(DogImageResult);
        _txt.text = menuName.ToString();
    }

    protected override void OnHideBefore()
    {
        _signal.TryUnsubscribe<DogImageResponseSignal>(DogImageResult);
    }

    private void DogImageResult(DogImageResponseSignal e)
    {
        _serviceResultTxt.text = e.Response.message;
    }
}
```


