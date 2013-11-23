var Mainichi = Mainichi || {};
Mainichi.ViewModels = Mainichi.ViewModels || {};

Mainichi.ViewModels.Index = function () {
    var self = this;
    
    var finder = new Booking.Services.OptionsFinder(options);
    var model = new Booking.Models.PageState(options, finder, partySize);
    
    this.products = new Booking.ViewModels.RoomTypes(model);
};

ko.applyBindings(new Booking.ViewModels.Index());