using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace CustomValidators
{
    /// <summary>
    /// Utility Layer class for Required Field validation
    /// </summary>
    public class RequiredFieldValidatorForCheckBoxLists : System.Web.UI.WebControls.BaseValidator
    {
        private ListControl _listctrl;

        public RequiredFieldValidatorForCheckBoxLists()
        {
            base.EnableClientScript = false;
        }

        protected override bool ControlPropertiesValid()
        {
            Control ctrl = FindControl(ControlToValidate);

            if (ctrl != null)
            {
                _listctrl = (ListControl)ctrl;
                return (_listctrl != null);
            }
            else
                return false;  // raise exception
        }

        protected override bool EvaluateIsValid()
        {
            return _listctrl.SelectedIndex != -1;
        }
    }

}