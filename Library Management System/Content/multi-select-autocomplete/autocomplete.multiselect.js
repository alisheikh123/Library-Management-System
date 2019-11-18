$.widget("ui.autocomplete", $.ui.autocomplete, {
    options : $.extend({}, this.options, {
        multiselect: false
    }),
    _create: function(){
        this._super();

        var self = this,
            o = self.options;
        var itemss = [];

        if (o.multiselect) {
         
           
            self.selectedItems = {};           
            self.multiselect = $("<div></div>")
                .addClass("ui-autocomplete-multiselect ui-state-default ui-widget")
                .css("width", self.element.width())
                .insertBefore(self.element)
                .append(self.element)
                .bind("click.autocomplete", function(){
                    self.element.focus();
                });
            
            var fontSize = parseInt(self.element.css("fontSize"), 10);
            function autoSize(e){
                var $this = $(this);
                $this.width(1).width(this.scrollWidth + fontSize - 1);

            };

            var kc = $.ui.keyCode;
            self.element.bind({
                "keydown.autocomplete": function(e){
                    if ((this.value === "") && (e.keyCode == kc.BACKSPACE)) {
                        var prev = self.element.prev();
                        o.source.push(prev.text());
                        itemss.splice($.inArray(prev.text(), itemss), 1);
                        o.recive(itemss);
                        delete self.selectedItems[prev.text()];
                        prev.remove();
                      
                    }
                },
                "focus.autocomplete blur.autocomplete": function(){
                    self.multiselect.toggleClass("ui-state-active");
                },
                "keypress.autocomplete change.autocomplete focus.autocomplete blur.autocomplete": autoSize
            }).trigger("change");

            o.select = function(e, ui) {
                $("<div></div>")
                    .addClass("ui-autocomplete-multiselect-item")
                    .text(ui.item.label)
                    .append(
                        $("<span></span>")
                            .addClass("ui-icon ui-icon-close")
                            .click(function(){
                                var item = $(this).parent();
                                
                                o.source.push(item.text());
                                
                                delete self.selectedItems[item.text()];
                                itemss.splice($.inArray(item.text(), itemss), 1);
                                item.remove();
                                
                                o.recive(itemss);
                               
                            })
                    )
                    .insertBefore(self.element);
                
                self.selectedItems[ui.item.label] = ui.item;
                
                itemss.push(ui.item.value);
                o.source.splice($.inArray(ui.item.value, o.source),1);
                
               
                o.recive(itemss);
               
                self._value("");
                return false;
            }
        }

        return this;
    }
});