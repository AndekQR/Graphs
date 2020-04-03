using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

public class MyJsonFormatter : JsonMediaTypeFormatter {

    public MyJsonFormatter() {
        this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        this.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
    }

    public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType) {
        base.SetDefaultContentHeaders(type, headers, mediaType);
        headers.ContentType = new MediaTypeHeaderValue("application/json");
    }
}