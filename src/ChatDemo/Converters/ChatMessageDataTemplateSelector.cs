using ChatDemo.Models;
using ChatDemo.ViewCells;
using Xamarin.Forms;

namespace ChatDemo.Converters
{
    public class ChatMessageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate InboundTemplate => new DataTemplate(typeof(ChatMessageInboundCell));
        public DataTemplate OutboundTemplate => new DataTemplate(typeof(ChatMessageOutboundCell));

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
            => ((ChatMessage)item).IsInbound ? InboundTemplate : OutboundTemplate;
    }
}