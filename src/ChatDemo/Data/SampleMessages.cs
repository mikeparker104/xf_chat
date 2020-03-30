using System;
using System.Collections.Generic;
using System.Linq;
using ChatDemo.Models;

namespace ChatDemo.Data
{
    // Lorem ipsum text list from: https://www.lipsum.com/feed/html
    internal static class SampleMessages
    {
        static Random _random = new Random(0);

        static List<string> _messageSnippets = new List<string>()
        {
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
            "Sed vitae eros dictum velit luctus ullamcorper quis cursus quam.",
            "Sed mollis velit ut hendrerit ullamcorper.",
            "Sed id ante vitae lacus condimentum mattis id quis metus.",
            "Maecenas venenatis dui vel massa facilisis, a auctor turpis feugiat.",
            "Ut scelerisque risus et dolor feugiat, bibendum bibendum quam feugiat.",
            "Nunc non tortor sit amet mauris eleifend eleifend id ac est.",
            "Nullam finibus dui vitae eros auctor facilisis.",
            "Phasellus sed risus eget arcu commodo facilisis laoreet viverra nisl.",
            "Integer vel turpis et felis faucibus consectetur congue vulputate purus.",
            "Cras vehicula arcu vel ex molestie venenatis.",
            "Sed interdum nisi a ligula finibus pharetra eget quis dui.",
            "In ultricies mauris vel nulla pharetra faucibus.",
            "Vestibulum quis mi nec arcu pellentesque efficitur.",
            "Fusce congue velit et gravida rhoncus.",
            "Integer in quam et mauris eleifend blandit.",
            "Curabitur sit amet est non elit efficitur dignissim id sit amet lectus.",
            "In ut orci rhoncus, molestie neque nec, convallis justo.",
            "Pellentesque lobortis elit eget elementum elementum.",
            "Ut faucibus tortor in purus aliquet, et facilisis dui aliquet.",
            "Maecenas porttitor nunc id velit rhoncus tincidunt.",
            "Nulla semper dolor sit amet congue molestie.",
            "Donec aliquet metus eu urna sagittis, sit amet porttitor nisl facilisis.",
            "Proin eu dui congue, pretium risus vel, tempor nisi.",
            "Sed vel erat scelerisque risus suscipit cursus nec sit amet arcu.",
            "Nam vel ligula sed neque auctor malesuada.",
            "In sed justo tincidunt, convallis tellus eu, pellentesque neque.",
            "Sed suscipit ligula faucibus, pulvinar tortor sed, auctor diam.",
            "Sed congue felis nec condimentum feugiat.",
            "Aliquam feugiat lorem ut arcu ullamcorper, lobortis facilisis metus scelerisque.",
            "Suspendisse vitae nunc vel diam tristique condimentum aliquet sed magna.",
            "Fusce vitae erat eu ante faucibus lobortis non venenatis nisl.",
            "Curabitur gravida diam quis tortor rutrum vestibulum.",
            "Praesent rutrum orci quis quam suscipit tristique."
        };

        internal static IEnumerable<ChatMessage> GenerateMessages(int items)
        {
            Random random = new Random(0);
            var startDateTime = DateTime.Now.Subtract(TimeSpan.FromHours(1));

            return Enumerable.Range(0, items).Select(i =>
                new ChatMessage
                {
                    Text = SampleMessages.GenerateMessageText(),
                    IsInbound = i % 2 != 0,
                    Timestamp = startDateTime += TimeSpan.FromMinutes(random.Next(1, 6))
                });
        }

        internal static string GenerateMessageText()
        {
            var shortSnippet = _messageSnippets.ElementAt(_random.Next(0, _messageSnippets.Count));

            if (_random.Next(0, 2) != 1)
                return shortSnippet;

            return $"{shortSnippet} {_messageSnippets.ElementAt(_random.Next(0, _messageSnippets.Count))}";
        }
    }
}