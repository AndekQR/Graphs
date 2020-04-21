namespace Client.helpers {
    public static class ObjectExtensions {
        public static bool IsNotNull(this object toCheck) {
            return toCheck != null;
        }
        public static bool IsNull(this object toCheck) {
            return toCheck == null;
        }
    }
}