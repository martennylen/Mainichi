var Mainichi = Mainichi || {};
Mainichi.ViewModels = Mainichi.ViewModels || {};
Mainichi.ViewModels.Admin = Mainichi.ViewModels.Admin || {};

Mainichi.ViewModels.Admin.Thing = function () {
    var self = this;

    self.thingId = ko.observable(Mainichi.ViewModels.Admin.Models.Thing.Id || {});
    self.isEditing = !_.isEmpty(self.thingId());
    self.slidesRaw = ko.observableArray(Mainichi.ViewModels.Admin.Models.Thing.Slides || []);
    self.indexList = _.range(5);
    self.attributes = ko.observableArray(Mainichi.ViewModels.Admin.Models.Thing.Attributes || []);
    
    var resetIndexes = function (deletedIndex) {
        _.each(self.slides(), function(p, index) {
            if (deletedIndex && index > deletedIndex) {
                p.Index(index - 1);
            } else {
                p.Index(index);
            }
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
        if (p.IsNew) {
            self.slides.splice(p.Index(), 1);
            resetIndexes();
        } else { //Mark as deleted
            p.DeleteMe(true);
            resetIndexes(p.Index());
        }
    };

    self.addImage = function() {
        self.slides.push(createSlide({}, self.slides().length));
    };

    self.addAttribute = function() {
        self.attributes.push( { 'Name': '', 'Value': '' });
    };
    
    self.removeAttribute = function (p) {
        self.attributes.splice(self.attributes.indexOf(p), 1);
    };

    self.hasImages = ko.computed(function() {
        return self.slides().length > 1;
    });
};

Mainichi.ViewModels.Admin.EditableSlide = function (model, index) {
    var self = this;

    if (_.isEmpty(model)) {
        model.FileName = 'placeholder.png';
        model.Text = 'Bildtext';
        model.Index = index;
        model.IsNew = true;
    };
    
    self.OriginalFileName = model.FileName;
    self.FileName = ko.observable(model.FileName);
    self.Text = ko.observable(model.Text);
    self.Index = ko.observable(model.Index);
    self.DeleteMe = ko.observable(model.DeleteMe);
    
    self.IsNew = model.IsNew;

    self.imageHasChanged = ko.computed(function() {
        return self.OriginalFileName !== self.FileName();
    });

    self.isEditing = ko.observable(false);
    self.editSelectedSlide = function () {
        self.isEditing(!self.isEditing());
    };
    
    self.editImage = function () {
        document.getElementsByName('Slides[' + self.Index() + '].File')[0].click();
    };

    self.populateFileName = function (f) {
        self.FileName(f.split('\\')[2]);
    };
};

ko.applyBindings(new Mainichi.ViewModels.Admin.Thing());