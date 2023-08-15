using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EventSite.TagHelpers
{
   
    [HtmlTargetElement(Attributes = "[type=submit]")]
    [HtmlTargetElement("a", Attributes = "my-button")]
    public class ButtonTagHelper : TagHelper
    {
        public bool MyButton { get; set; }  
        public override void Process(TagHelperContext context, 
        TagHelperOutput output)
        {
            output.Attributes.AppendCssClass("btn btn-primary");
        }
    }

}
