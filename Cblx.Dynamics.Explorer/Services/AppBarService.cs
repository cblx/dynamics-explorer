using Microsoft.AspNetCore.Components;

namespace Cblx.Dynamics.Explorer.Services;

public class AppBarService
{
    private RenderFragment? _content;

    public event Action? OnContentChanged;

    public RenderFragment? Content { 
        get => _content; 
        set
        {
            _content = value;
            OnContentChanged?.Invoke();
        }
    }
}
