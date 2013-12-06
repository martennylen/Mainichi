var Mainichi = Mainichi || {};
Mainichi.ViewModels = Mainichi.ViewModels || {};
Mainichi.ViewModels.Admin = Mainichi.ViewModels.Admin || {};

Mainichi.ViewModels.Admin.Thing = function () {
    var self = this;

    self.thingId = ko.observable(Mainichi.ViewModels.Admin.Models.Thing.Id || {});
    self.isEditing = !_.isEmpty(self.thingId());
    self.slidesRaw = ko.observableArray(Mainichi.ViewModels.Admin.Models.Thing.Slides || {});
    self.indexList = _.range(5);
    
    var resetIndexes = function () {
        _.each(self.slides(), function (p, index) {
            p.Index(index);
        });
    };

    var createSlide = function (model, parent) {
        return new Mainichi.ViewModels.Admin.EditableSlide(model, parent);
    };

    self.slides = ko.observableArray(
        _.map(self.slidesRaw(), function (p, index) {
            return createSlide(p, index);
        })
    );
    
    self.addButtonText = ko.computed(function() {
        return self.isEditing ? 'Uppdatera produkt' : 'Lägg till produkt';
    });
    
    self.removeImage = function (p) {
        console.log(p);
        self.slides.splice(p.Index(), 1);
        resetIndexes();
        console.log(self.slides());
    };

    self.addImage = function() {
        self.slides.push(createSlide({}, self.slides().length));
    };
};

Mainichi.ViewModels.Admin.EditableSlide = function (model, index) {
    var self = this;

    if (_.isEmpty(model)) {
        model.FileName = 'placeholder.png';
        model.Text = 'Bildtext';
        model.Index = index;
    };
    
    self.OriginalFileName = model.FileName;
    self.FileName = ko.observable(model.FileName);
    self.Text = ko.observable(model.Text);
    self.Index = ko.observable(model.Index);

    self.imageHasChanged = ko.computed(function() {
        return self.OriginalFileName !== self.FileName();
    });

    self.isEditing = ko.observable(false);
    self.editSelectedSlide = function () {
        self.isEditing(!self.isEditing());
    };
    
    self.isImageEditing = ko.observable(false);
    self.editImage = function () {
        self.isImageEditing(!self.isImageEditing());
    };

    self.populateFileName = function (f) {
        self.FileName(f.split('\\')[2]);
        self.editImage();
    };
};

ko.applyBindings(new Mainichi.ViewModels.Admin.Thing());