function () {


    label = $FILTER$;/*function (o) {
        return o["concept:name"].startsWith("SRM");
    }*/

    for (var i = 0; i < this.Items.length; i++) {
        if (label(this.Items[i])) {
            emit(this._id, { data: [i] });
        }
    };
}