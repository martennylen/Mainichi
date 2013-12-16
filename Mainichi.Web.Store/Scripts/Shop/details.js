var Mainichi = Mainichi || {};
Mainichi.ViewModels = Mainichi.ViewModels || {};
Mainichi.ViewModels.Shop = Mainichi.ViewModels.Shop || {};

Mainichi.ViewModels.Shop.Details = function () {
    var self = this;
    self.slides = ko.observableArray(Mainichi.ViewModels.Details.Models.Slides || []);
    self.currentImage = ko.observable();
    self.imageContainer = document.getElementById('head-honcho');

    self.nextSlide = function () {
        if (self.currentImage().Index < self.slides().length - 1) {
            self.currentImage(self.slides()[(self.currentImage().Index) + 1]);
        } else {
            self.currentImage(self.slides()[0]);
        }
    };

    self.previousSlide = function () {
        if (self.currentImage().Index > 0) {
            self.currentImage(self.slides()[(self.currentImage().Index) - 1]);
        } else {
            self.currentImage(self.slides()[self.slides().length - 1]);
        }
    };

    self.setSlide = function(p) {
        self.currentImage(p);
    };

    //ko.computed({
    //    read: function () {
    //        self.currentImage();
    //        var h = self.imageContainer.offsetHeight;
    //        if (h > 0) {
    //            self.imageContainer.previousSibling.previousSibling.style.top = (h * 0.45) + 'px';
    //            self.imageContainer.nextSibling.nextSibling.style.top = (h * 0.45) + 'px';
    //        }
    //    }, deferEvaluation: true
    //}).extend({ throttle: 50 });

    self.currentImage(_.first(self.slides()));
};

ko.applyBindings(new Mainichi.ViewModels.Shop.Details());