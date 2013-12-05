var Mainichi = Mainichi || {};
Mainichi.ViewModels = Mainichi.ViewModels || {};
Mainichi.ViewModels.Admin = Mainichi.ViewModels.Admin || {};

Mainichi.ViewModels.Admin.Thing = function () {
    var self = this;

    self.thingId = ko.observable(Mainichi.ViewModels.Admin.Models.Thing.Id || {});
    self.isEditing = !_.isEmpty(self.thingId());
    self.slidesRaw = ko.observableArray(Mainichi.ViewModels.Admin.Models.Thing.Slides || {});

    self.slides = ko.computed(function() {
        return _.map(self.slidesRaw(), function(p) {
            return new Mainichi.ViewModels.Admin.EditableSlide(p, self);
        });
    });
    
    self.addButtonText = ko.computed(function() {
        return self.isEditing ? 'Uppdatera produkt' : 'Lägg till produkt';
    });

    self.isImageEditing = ko.observable(false);
    self.editImage = function() {
        self.isImageEditing(!self.isImageEditing());
    };
    
    self.removeImage = function (p) {
        self.slidesRaw.splice(p.data().Index, 1);
        resetIndexes();
    };

    var resetIndexes = function() {
        _.each(self.slidesRaw(), function(p, index) {
            p.Index = index;
        });
    };

    self.addImage = function() {
        self.slidesRaw.push({});
        console.log(self.slidesRaw());
    };
};

Mainichi.ViewModels.Admin.EditableSlide = function (p, pp) {
    var self = this;

    if (_.isEmpty(p)) {
        p.FileName = 'placeholder.png';
        p.Text = 'Bildtext';
        p.Index = pp.slidesRaw().length-1;
    };
    
    self.data = ko.observable(p);

    self.isEditing = ko.observable(false);
    self.editSelectedSlide = function () {
        self.isEditing(!self.isEditing());
    };
};

ko.applyBindings(new Mainichi.ViewModels.Admin.Thing());