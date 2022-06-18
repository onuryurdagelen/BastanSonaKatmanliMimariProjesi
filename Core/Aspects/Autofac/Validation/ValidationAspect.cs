using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception //Aspect
    {
        //Attribute ile calisirken Type ile calismak zorundayiz.
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            //defensive coding
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir dogrulama sinifi degil.");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            //invocation ==> metot demektir.
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //Reflection vardir.
            //Product Validator.
            //Reflection ==> Calisma aninda bir seyleri calistirmayi saglar.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            //Product
            //ProductValidator'un calisma tipini bul diyor ardindan base tipini bul ve generic calisma tipini bul demektir.
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            //Ardindan parametrelerini bul.Ornek Add metodundaki parametrenin tipi ile entity'nin parametresi ayni olani bul
            //Birden fazla parametre olabilir.Bu nedenle foreach kullanilir.
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
