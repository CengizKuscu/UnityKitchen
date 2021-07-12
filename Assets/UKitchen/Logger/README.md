## Logger

Run with ```APP_LOG``` Scripting Define Symbol. Type to under Scripting Compilation from ```Project Settings -> Player -> Other Settings```.

use like that: ```AppLog.Info(<some object>, <args>);```

```c#
public class SampleClass : MonoBehavior
{
    public void SampleMethod()
    {
        AppLog.Info(this, "Hello", 3, gameObject.name, gameObject.tag);
        AppLog.Info("SAMPLE INFO LOG", "Hello", 3, gameObject.name, gameObject.tag);
        
        AppLog.Warning(this, "Hello", 3, gameObject.name, gameObject.tag);
        AppLog.Warning("SAMPLE INFO LOG", "Hello", 3, gameObject.name, gameObject.tag);
        
        AppLog.Error(this, "Hello", 3, gameObject.name, gameObject.tag);
        AppLog.Error("SAMPLE INFO LOG", "Hello", 3, gameObject.name, gameObject.tag);  
    }
} 
```
