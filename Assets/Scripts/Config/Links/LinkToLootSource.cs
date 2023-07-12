using CozyServer.DTS.Links;
using System;

namespace ZombieFarm.Config.Links
{
    [Serializable]
    public struct LinkToLootSource : ILink
    {
        public string LinkedObjectId => itemId;

        public string itemId;

        private int cachedHashCode;

        public bool HasValue => Helper.HasValue(itemId);

        public static bool operator ==(LinkToLootSource a, LinkToLootSource b) => a.HasValue && b.HasValue && string.Equals(a.itemId, b.itemId);
        public static bool operator !=(LinkToLootSource a, LinkToLootSource b) => a == b == false;
        public override bool Equals(object obj) => obj is LinkToLootSource link && this == link;
        public override int GetHashCode() => Helper.GetHashCode(ref cachedHashCode, itemId);
        public override string ToString() { return $"{nameof(LinkToLootSource)}: {LinkedObjectId}"; }
    }
}
