using System.Collections;
using System.Collections.Generic;

public class Info
{   
    public int totalNumber = 9;
    static private Info instance;
    static public Info Instance 
    {
        get
        {
            if(null == instance)
            {
                Info inst = new Info();
                if(null != inst)
                {
                    instance = inst;
                }
            }
            return instance;
        }
    }

    private Info()
    {

    }
}
