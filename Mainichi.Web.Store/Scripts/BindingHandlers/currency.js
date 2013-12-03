ko.bindingHandlers.currency = {
    update: function (element, valueAccessor) {
        var totalPrice = ko.utils.unwrapObservable(valueAccessor());
        var currencySuffix = ':-';
        
        var newValueAccessor = function () {
            if (isNaN(totalPrice) || $.trim(totalPrice.toString()).length == 0 || totalPrice.toString().length <= 3) {
                return totalPrice + currencySuffix;
            }

            return parseInt(totalPrice).splitThousands() + currencySuffix;
        };

        ko.bindingHandlers.text.update(element, newValueAccessor);
    }
};

String.prototype.format = function () {
    var args = arguments;
    return this.replace(/{(\d+)}/g, function (match, number) {
        return typeof args[number] != 'undefined' ? args[number] : match;
    });
};

Number.prototype.splitThousands = function () {
    var s = this.toString();
    var thousands = Math.floor(this / 1000).toString();
    return this > 999 ? ("{0} {1}").format(thousands, s.slice(thousands.length, s.length)) : s;
};