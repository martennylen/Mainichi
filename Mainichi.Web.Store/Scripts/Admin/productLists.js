var Mainichi = Mainichi || {};
Mainichi.ViewModels = Mainichi.ViewModels || {};
Mainichi.ViewModels.Admin = Mainichi.ViewModels.Admin || {};

Mainichi.ViewModels.Admin.ProductLists = function() {
    var self = this;
    self.listTypes = [{'Id': 'new', 'Name': 'Nya'}, {'Id': 'featured', 'Name': 'Utvalda'}, {'Id': 'discounted', 'Name': 'Nedsatta'}];
    self.selectedListType = ko.observable(self.listTypes[0].Id);
    self.productListRaw = ko.observableArray(Mainichi.ViewModels.Admin.Models.ProductLists.InitialListItems);
    self.actionText = ko.observable('');
    self.allThings = Mainichi.ViewModels.Admin.Models.ProductLists.AllLists;

    self.selectedListType.subscribe(function(newValue) {
        $.getJSON('/api/ThingsLists/' + newValue, function(d) {
            self.productListRaw(d);
        });
    });

    self.productList = ko.computed(function() {
        return _.map(self.productListRaw(), function(p) {
            return new Mainichi.ViewModels.Admin.EditableProduct(p, self);
        });
    });

    var mapIdentificators = function () {
        return _.map(self.productList(), function(p) {
            return p.data().Id;
        });
    };

    self.saveSelectedProducts = function() {
        var possiblyNewIdentificators = mapIdentificators();
        if (!arrayEq(selectedIdentificators, possiblyNewIdentificators)) {
            selectedIdentificators = possiblyNewIdentificators;
            $.post("/Admin/UpdateSelectedProducts", $.param({ Id: self.selectedListType(), ThingIds: selectedIdentificators }, true)).done(function(reply) {
                if (reply) {
                    self.actionText(reply.status);
                }
            });
        } else {
            self.actionText('Listan har inte ändrats.');
        }
    };

    var selectedIdentificators = mapIdentificators();
};

arrayEq = function(a, b) {
    return _.every(_.zip(a, b), function(x) {
        return x[0] === x[1];
    });
};

Mainichi.ViewModels.Admin.EditableProduct = function(p, pp) {
    var self = this;

    self.data = ko.observable(p);

    self.isEditing = ko.observable(false);
    self.editSelectedProduct = function () {
        self.isEditing(!self.isEditing());
    };

    self.updateSelectedProduct = function() {
        if (typeof (self.data().Id) === "undefined") { //Välj en produkt
            self.data().Id = "things/0";
        }
        $.getJSON('/api/Things/' + self.data().Id.split('/')[1], function(d) {
            if (d !== null) {
                self.data(d);
                pp.actionText('');
            } else {
                pp.actionText('Produkten existerar inte');
            }
        });

        self.isEditing(false);
    };
};

ko.applyBindings(new Mainichi.ViewModels.Admin.ProductLists());