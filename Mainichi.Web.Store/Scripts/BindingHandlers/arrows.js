ko.bindingHandlers.arrows = {
    //init: function (element, v) {
    //console.log('init');
    //var apa = ko.utils.unwrapObservable(v());
    ////    //console.log(apa);
    ////    ////$(element).attr({ src: '/Content/Snapshots/Products/' + apa.FileName });
    ////    var apa = ko.computed(function () {
    ////        console.log('nu');
    //        $(element).siblings('span').css('top', element.offsetHeight * 0.45);
    //    }).extend({throttle: 1500});

    //    apa();
    //},
    update: function (element, valueAccessor) {
        console.log('update');
        var currentImage = ko.utils.unwrapObservable(valueAccessor());
        var el = $(element);
        el.siblings('span').css('top', el.height() * 0.45);
    }
};