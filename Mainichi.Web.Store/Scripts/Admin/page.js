var Mainichi = Mainichi || {};
Mainichi.ViewModels = Mainichi.ViewModels || {};
Mainichi.ViewModels.Admin = Mainichi.ViewModels.Admin || {};

Mainichi.ViewModels.Admin.Index = function () {
    var self = this;

    self.thingId = ko.observable(Mainichi.ViewModels.Admin.Models.Thing.Id || {});
    self.isEditing = !_.isEmpty(self.thingId());
    self.thingImage = ko.observable(Mainichi.ViewModels.Admin.Models.Thing.Image || {});
    
    self.addButtonText = ko.computed(function() {
        return self.isEditing ? 'Uppdatera produkt' : 'Lägg till produkt';
    });

    self.isImageEditing = ko.observable(false);
    self.removeImage = function() {
        self.isImageEditing(!self.isImageEditing());
    };
};

ko.applyBindings(new Mainichi.ViewModels.Admin.Index());