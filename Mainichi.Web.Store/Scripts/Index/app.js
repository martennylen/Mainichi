var Mainichi = Mainichi || {};
Mainichi.ViewModels = Mainichi.ViewModels || {};

Mainichi.ViewModels.Index = function () {
    var self = this;

    //var finder = new Booking.Services.OptionsFinder(options);
    //var model = new Booking.Models.PageState(options, finder, partySize);
    self.productlist = ko.observable();
    
    (Sammy('#the-magic-happens-here', function () {
        this.get('#/', function (context) {
            this.load('/Scripts/Data/products.json')
            .then(function (items) {
                self.productlist(items);
            });
        });
        
        this.get('#/thing/:slug/:id', function (context) {
            context.log(context.params['id']);
        });
    })).run('#/');
};

ko.applyBindings(new Mainichi.ViewModels.Index());