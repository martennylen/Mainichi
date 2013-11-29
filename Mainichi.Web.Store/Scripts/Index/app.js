var Mainichi = Mainichi || {};
Mainichi.ViewModels = Mainichi.ViewModels || {};

Mainichi.ViewModels.Index = function () {
    var self = this;

    //var finder = new Booking.Services.OptionsFinder(options);
    //var model = new Booking.Models.PageState(options, finder, partySize);
    self.productlist = ko.observableArray();
    
    (Sammy('#the-magic-happens-here', function () {
        this.get('#/', function (context) {
            this.load('/api/Things', { json: true })
            .then(function (items) {
                self.productlist(items);
            });
        });
        
        //this.get('#/browse/:category', function (context) {
        //    context.log(context.params['category']);
        //});

        this.get(/\#\/browse\/(.*)/, function(context) {
            context.log(_.last(context.params['splat'][0].split('/')));
        });
    })).run('#/');
};

ko.applyBindings(new Mainichi.ViewModels.Index());