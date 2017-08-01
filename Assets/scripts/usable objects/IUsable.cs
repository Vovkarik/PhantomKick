using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsable{

    void Use(PlayerController character);
    void Highlight(bool t);
    Vector3 GetPosition();
    void ActiveAbility();
    void LeaveObject();
    bool IsPossessed();
    bool HasAbility();
    void Swipe();
}
