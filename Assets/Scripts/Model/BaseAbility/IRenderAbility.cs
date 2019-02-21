using UnityEngine;

namespace Model.Ability
{
    /// <summary>
    /// This ability allows object to add renderable object to the scene 
    /// </summary>
    public interface IRenderAbility : IBaseAbility
    {
        GameObject GetRenderObject();
    }
}