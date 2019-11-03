using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XF_Nodejs.Models;

namespace XF_Nodejs.CustumViewCell
{
    public class ChatDataTemplate : DataTemplateSelector
    {
        private readonly DataTemplate InputText_Chat=new DataTemplate(type:typeof(InputTextViewCell));
        private readonly DataTemplate OutputText_Chat= new DataTemplate(type: typeof(OutputTextViewCell));

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Messenger_Model messenger_Model = new Messenger_Model();
            messenger_Model = (Messenger_Model)item;
            if (!messenger_Model.isInputChat)
            {
                return InputText_Chat;
            }
            else
            {
                return OutputText_Chat;
            }

        }
    }
}
