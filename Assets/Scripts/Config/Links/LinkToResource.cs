using CozyServer.DTS.Links;
using System;

namespace ZombieFarm.Config.Links
{
    [Serializable]
    public struct LinkToResource : ILink
    {
        public string LinkedObjectId => itemId;

        public string itemId;

        private int cachedHashCode;

        public bool HasValue => Helper.HasValue(itemId);

        public static bool operator ==(LinkToResource a, LinkToResource b) => a.HasValue && b.HasValue && string.Equals(a.itemId, b.itemId);
        public static bool operator !=(LinkToResource a, LinkToResource b) => a == b == false;
        public override bool Equals(object obj) => obj is LinkToResource link && this == link;
        public override int GetHashCode() => Helper.GetHashCode(ref cachedHashCode, itemId);
        public override string ToString() { return $"{nameof(LinkToResource)}: {LinkedObjectId}"; }
    }
}
