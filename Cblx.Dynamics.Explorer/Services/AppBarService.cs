using Microsoft.AspNetCore.Components;

namespace Cblx.Dynamics.Explorer.Services;

public class AppBarService
{
    public Action? ContentChanged { get; set; }
    public RenderFragment? Content { get; private set; }
    public void SetContent(RenderFragment? content)
    {
        Content = content;
        if(ContentChanged is null) { return; }
        ContentChanged();
    }
}
