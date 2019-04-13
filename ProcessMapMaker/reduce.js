function (key, values) {


    var res = { data: [] };
    values.forEach(function (val) {
        res.data = res.data.concat(val.data);
    });
    return res;
}