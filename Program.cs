using System;
using System.Collections.Generic;
namespace chpt2
{
    class Program
    {
        class Url {
            private string uri { get; }
            private List<string> uriParams { get; set; }
            public List<string> uriArgs { get; set; }
            public string baseUri { get; }
            public List<string> tokens { get; set; }
            public Url(string uri) {
                this.uri = uri;
                this.baseUri = uri.Remove(uri.IndexOf('?'), uri.Length - uri.IndexOf('?'));
                SplitUriTokens();
                this.uriParams = GetParams();
                this.uriArgs = GetArgs();
            }
            private void SplitUriTokens() {
                var questionIndex = uri.IndexOf('?');
                var tokens = new List<string>();

                if (questionIndex < 0) {
                    this.tokens = tokens;
                }
                var tokenString = uri.Remove(0, questionIndex + 1);
                foreach (var token in tokenString.Split('&')) {
                    tokens.Add(token);
                }
                this.tokens = tokens;
            }
            public void PrintParams() {
                
                foreach (var param in uriParams) {
                    Console.WriteLine(param);
                }
            }
            public void PrintTokens() {
                foreach (var token in tokens) {
                    Console.WriteLine(token);
                }
            }
            public void PrintArgs() {
                foreach (var arg in uriArgs) {
                    Console.WriteLine(arg);
                } 
            }
            public void PrintFuzzed() {
                foreach (var uri in GetFuzzedUrls()) {
                    Console.WriteLine(uri);
                }
            }
            public List<string> GetParams() {
                var output = new List<string>();
                foreach (var token in tokens) {
                    var equalsIndex = token.IndexOf('=');            
                
                    var param = token.Remove(equalsIndex, token.Length - equalsIndex);
                    output.Add(param);
                }
                return output;
            }
            public List<string> GetArgs() {
                var output = new List<string>();
                foreach (var token in tokens) {
                    var equalsIndex = token.IndexOf('=');
                    var arg = token.Remove(0, equalsIndex + 1);
                    output.Add(arg);
                }
                return output;
            }
            public List<string> GetFuzzedUrls() {
                var output = new List<string>();
                foreach (var arg in uriArgs) {
                    output.Add(uri.Replace(arg, arg + "fd<xss>sa"));
                    output.Add(uri.Replace(arg, arg + "fd'sa"));
                }   

                return output;
            }
        }
        static void Main(string[] args)
        {
            var url = new Url(args[0]);
            url.PrintTokens();
            
            url.PrintParams();
            url.PrintArgs();

            url.PrintFuzzed();
        }
    }
}
