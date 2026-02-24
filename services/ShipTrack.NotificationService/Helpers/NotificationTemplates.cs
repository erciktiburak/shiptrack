namespace ShipTrack.NotificationService.Helpers;

public static class NotificationTemplates
{
    public static string ShipmentCreated(string receiverName, string trackingNumber, string origin, string destination) =>
        $"Dear {receiverName}, your shipment {trackingNumber} has been created. " +
        $"It will be shipped from {origin} to {destination}. " +
        $"You can track your shipment using the tracking number.";

    public static string ShipmentCancelled(string trackingNumber) =>
        $"Your shipment with tracking number {trackingNumber} has been cancelled. " +
        $"Please contact us if you have any questions.";

    public static string StatusChanged(string trackingNumber, string newStatus, string location) =>
        $"Your shipment {trackingNumber} status has been updated to {newStatus}. " +
        $"Current location: {location}.";
}