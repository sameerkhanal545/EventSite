using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;       
using Microsoft.AspNetCore.Mvc.Rendering;          

namespace EventSite.TagHelpers
{
    [HtmlTargetElement("dismissable-message", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class DismissableMessageTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			var td = ViewCtx.TempData;
			if (td.ContainsKey("message"))
			{
				output.TagName = "div";
				output.Attributes.SetAttribute("class", "alert alert-info alert-dismissible fade show text-center font-weight-bold");
				output.Attributes.SetAttribute("role", "alert");

				var closeButton = new TagBuilder("button");
				closeButton.Attributes.Add("type", "button");
				closeButton.Attributes.Add("class", "close");
				closeButton.Attributes.Add("data-dismiss", "alert");
				closeButton.InnerHtml.AppendHtml("<span>&times;</span>");

				var message = new TagBuilder("p");
				message.InnerHtml.Append(td["message"].ToString());

				output.Content.AppendHtml(closeButton);
				output.Content.AppendHtml(message);

				var dismissScript = "<script>setTimeout(function() { $('.alert').alert('close'); }, 3000);</script>";
				output.PostContent.AppendHtml(dismissScript);
			}
			else
			{
				output.SuppressOutput();
			}
		}
	}
}
