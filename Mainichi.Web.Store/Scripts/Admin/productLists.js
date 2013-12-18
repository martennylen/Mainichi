var Mainichi = Mainichi || {};
Mainichi.ViewModels = Mainichi.ViewModels || {};
Mainichi.ViewModels.Admin = Mainichi.ViewModels.Admin || {};

Mainichi.ViewModels.Admin.ProductLists = function() {
    var self = this;
    self.listTypes = [{'Id': 'new', 'Name': 'Nya'}, {'Id': 'featured', 'Name': 'Utvalda'}, {'Id': 'discounted', 'Name': 'Nedsatta'}];
    self.currentListModel = ko.observable(Mainichi.ViewModels.Admin.Models.ProductLists.InitialList);
    self.actionText = ko.observable('');
    self.allThings = Mainichi.ViewModels.Admin.Models.ProductLists.AllThings;

    self.selectedListType = ko.observable(self.listTypes[0].Id);
    self.selectedListType.subscribe(function(newValue) {
        $.getJSON('/api/ThingLists/' + newValue, function(d) {
            self.currentListModel(d);
        });
    });

    self.currentList = ko.computed(function () {
        return {
            'Descriptor': ko.observable(self.currentListModel().Descriptor),
            'Active': ko.observable(self.currentListModel().Active),
            "Things": _.map(self.currentListModel().Things, function(p) {
                return new Mainichi.ViewModels.Admin.EditableProduct(p, self);
            })
        };
    });

    var mapIdentificators = function () {
        return _.map(self.currentList().Things, function(p) {
            return p.data().Id;
        });
    };
    
    var selectedIdentificators = mapIdentificators();

    self.isDirty = ko.computed(function() {
        var possiblyNewIdentificators = mapIdentificators();
        return (!arrayEq(selectedIdentificators, possiblyNewIdentificators) ||
            self.currentList().Descriptor() !== self.currentListModel().Descriptor ||
            self.currentList().Active() !== self.currentListModel().Active);
    });

    self.saveSelectedProducts = function() {
        $.post("/Admin/UpdateSelectedProducts", $.param({ Id: self.selectedListType(), Descriptor: self.currentList().Descriptor, Active: self.currentList().Active, ThingIds: mapIdentificators() }, true)).done(function (reply) {
            if (reply) {
                self.actionText(reply.status);
                _.delay(function() {
                    self.actionText('');
                }, 1000);
            }
        });
    };
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