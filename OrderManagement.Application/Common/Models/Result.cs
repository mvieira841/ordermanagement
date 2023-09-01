namespace OrderManagement.Application.Common.Models;

/// <summary>
/// Response
/// </summary>
public class Response
{
    /// <summary>
    /// Messages
    /// </summary>
    [JsonPropertyName("messages")]
    public List<Message> Messages { get; set; } = new List<Message>();

    /// <summary>
    /// Succeeded
    /// </summary>
    [JsonPropertyName("succeeded")]
    public bool Succeeded => Messages.Count(m => m.Type == MessageType.Error) == 0;

    public void AddSuccessMessage(string text)
    {
        AddMessage(MessageType.Success, text);
    }

    public void AddErrorMessage(string text)
    {
        AddMessage(MessageType.Error, text);
    }

    public void AddErrorMessage(Exception ex)
    {
        AddErrorMessage(ex.Message);
    }

    public void AddErrorMessages(List<string> texts)
    {
        Messages.AddRange(texts.Select(m => new Message { Type = MessageType.Error, Text = m }).ToArray());
    }

    public void AddInfoMessage(string text)
    {
        AddMessage(MessageType.Info, text);
    }

    public void AddWarningMessage(string text)
    {
        AddMessage(MessageType.Warning, text);
    }

    private void AddMessage(MessageType messageType, string message)
    {
        Messages.Add(new Message { Type = messageType, Text = message });
    }
}

/// <summary>
/// Response
/// </summary>
/// <typeparam name="T"></typeparam>
public class Response<T> : Response
{
    /// <summary>
    /// Data
    /// </summary>
    [JsonPropertyName("data")]
    public T Data { get; set; }
    public Response() { }

    public Response(T data)
    {
        Data = data;
    }
}
