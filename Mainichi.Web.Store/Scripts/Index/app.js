/*global Sammy, jQuery */

var Mainichi = Mainichi || {};
Mainichi.ViewModels = Mainichi.ViewModels || {};

Mainichi.ViewModels.Index = function () {
    var self = this;

    //var finder = new Booking.Services.OptionsFinder(options);
    //var model = new Booking.Models.PageState(options, finder, partySize);
    self.productlist = ko.observable();
    
    ($.sammy('#the-magic-happens-here', function () {
        this.get('#/', function (context) {
            this.load('/Scripts/Data/products.json')
            .then(function (items) {
                self.productlist(items);
            });
        });
    })).run('#/');
};

ko.applyBindings(new Mainichi.ViewModels.Index());