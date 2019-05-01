module.exports = function () {
    {
        this.handleReq = handleReq;
        return this;
    }
    function handleReq(req, res) {
        switch (req) {
            case "list": res.send({
                "result": [{ "time": "04:13", "day": "11", "month": "Jul", "size": "0", "group": "1331", "user": "jonas@guillermopercoco.com.ar", "number": "3", "rights": "drwxr-xr-x", "type": "dir", "name": "a", "date": "2017-07-11 05:09:32" },
                { "time": "03:42", "day": "11", "month": "Jul", "size": "0", "group": "1331", "user": "jonas@guillermopercoco.com.ar", "number": "2", "rights": "drwxr-xr-x", "type": "dir", "name": "asdads", "date": "2017-07-11 05:09:32" },
                { "time": "19:07", "day": "10", "month": "Jul", "size": "0", "group": "1331", "user": "jonas@guillermopercoco.com.ar", "number": "2", "rights": "drwxr-xr-x", "type": "dir", "name": "lop", "date": "2017-07-11 05:09:32" },
                { "time": "19:07", "day": "10", "month": "Jul", "size": "3605932", "group": "1331", "user": "jonas@guillermopercoco.com.ar", "number": "1", "rights": "-rw-r--r--", "type": "file", "name": "Src/Html/HomeImages/temp/shahbala.jpg", "date": "2017-07-11 05:09:32" }
                ]
            }).end();
            default:res.end()
        }

    }
}