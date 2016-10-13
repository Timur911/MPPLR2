using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ConsoleApplication1
{
    public class WeakDelegate
    {
        private MethodInfo method;
        private WeakReference targetReference;
        private Type typeOfDelegate;

        public WeakDelegate(Delegate realDelegate)
        {
            targetReference = new WeakReference(realDelegate.Target);
            method = realDelegate.Method;
            typeOfDelegate = realDelegate.GetType();
        }

        public bool IsAlive
        {
            get { return targetReference.IsAlive; }
        }

        public Delegate GetDelegate()
        {
            ParameterExpression[] paramArr = GetParametersExpression(method);
            Expression targetObjectExpression = GetPropertyExpression(Expression.Constant(targetReference), "Target", targetReference.Target.GetType());
            Expression targetMethodInvoke = Expression.Call(targetObjectExpression, method, paramArr);
            Expression conditionExpression = Expression.NotEqual(targetObjectExpression, Expression.Constant(null));
            Expression ifExpression = Expression.IfThen(conditionExpression, targetMethodInvoke);
            LambdaExpression labmda = Expression.Lambda(ifExpression, paramArr);
            return labmda.Compile();
        }

        private ParameterExpression[] GetParametersExpression(MethodInfo method)
        {
            ParameterInfo[] eventHandlerArgsInfo = method.GetParameters();
            ParameterExpression[] eventHandlerArgsExpressionMassive = new ParameterExpression[eventHandlerArgsInfo.Length];
            for (int i = 0; i < eventHandlerArgsInfo.Length; i++)
            {
                eventHandlerArgsExpressionMassive[i] = ParameterExpression.Parameter(eventHandlerArgsInfo[i].ParameterType);
            }
            return eventHandlerArgsExpressionMassive;
        }

        private Expression GetPropertyExpression(Expression declaryingObjectExpression, String propertyName, Type typeToCastProperty = null)
        {
            Type declaryingClassType = declaryingObjectExpression.Type;
            PropertyInfo targetPropertyInfo = declaryingClassType.GetProperty(propertyName);
            Expression targetObjectExpression = Expression.Property(declaryingObjectExpression, targetPropertyInfo);
            if (typeToCastProperty != null)
            {
                targetObjectExpression = Expression.Convert(targetObjectExpression, typeToCastProperty);
            }
            return targetObjectExpression;
        }

        public Delegate Weak
        {
            get
            {
                return GetDelegate();

            }
        }
    }
}
