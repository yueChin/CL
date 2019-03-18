using System;

public interface IAdventureEventAction {
    void Show();
    void Hide();
    void OnComplete(Action action);
}
