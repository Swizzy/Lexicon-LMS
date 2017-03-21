$.validator.addMethod("genericcompare",
    function(value, element, params) {
        var propelename = params.split(",")[0];
        var operName = params.split(",")[1];
        if (params.length === 0 ||
            value == undefined ||
            value.length === 0 ||
            propelename == undefined ||
            propelename.length === 0 ||
            operName == undefined ||
            operName.length === 0)
            return true;
        var valueOther = $(propelename).val();
        var val1 = (isNaN(value) ? Date.parse(value) : eval(value));
        var val2 = (isNaN(valueOther) ? Date.parse(valueOther) : eval(valueOther));

        if (operName === "GreaterThan")
            return val1 > val2;
        if (operName === "LessThan")
            return val1 < val2;
        if (operName === "GreaterThanOrEqual")
            return val1 >= val2;
        if (operName === "LessThanOrEqual")
            return val1 <= val2;
        if (operName === "Equal")
            return val1 === val2;
        if (operName === "NotEqual")
            return val1 !== val2;
        return true; // Default to true, we can't verify if we don't know how to verify it
    });

$.validator.unobtrusive.adapters.add("genericcompare",
    ["comparetopropertyname", "operatorname"],
    function(options) {
        options.rules["genericcompare"] = "#" +
            options.params.comparetopropertyname +
            "," +
            options.params.operatorname;
        var operstring = "";
        if (options.params.operatorname === "GreaterThan")
            operstring = "greater than";
        if (options.params.operatorname === "LessThan")
            operstring = "greater than or equal to";
        if (options.params.operatorname === "GreaterThanOrEqual")
            operstring = "less than";
        if (options.params.operatorname === "LessThanOrEqual")
            operstring = "less than or equal to";
        if (options.params.operatorname === "Equal")
            operstring = "equal to";
        if (options.params.operatorname === "NotEqual")
            operstring = "not equal to";

        function format(str, obj) {
            return str.replace(/\{\s*([^}\s]+)\s*\}/g, function(m, index, o, s) { return obj[index]; });
        }

        options.messages["genericcompare"] = format(options.message, [operstring]);
        
    });