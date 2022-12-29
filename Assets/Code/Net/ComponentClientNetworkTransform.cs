using Unity.Netcode.Components;

namespace Assets.Code.Net
{
    public class ComponentClientNetworkTransform : NetworkTransform
    {

        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}
