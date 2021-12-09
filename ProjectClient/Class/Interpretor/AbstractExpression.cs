using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient.Class.Interpretor
{
    public abstract class AbstractExpression
    {
        public abstract int execute();

        public AbstractExpression Interpret(Context context)
        {
            //if(context.Input.Length == 0)
            //{
            //    return;
            //}
            if(context.Input == "MOVE" || context.Input == "move" || context.Input == "m")
            {
                //AbstractExpression exp = new Command()
            }
            return null;
        }
    }
}
