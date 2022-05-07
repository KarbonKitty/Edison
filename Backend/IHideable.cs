using System;

namespace Edison;

public interface IHideable
{
    public Func<GameState, bool> RevealFunction { get; }
    public bool IsHidden { get; }
    public void Reveal();
}
