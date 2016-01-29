import express = require('express');
var router = express.Router();

import CheckoutPageRequestNS = require("./easpay.lib/CheckoutPageRequest");
import EasypayRequestNS = require("./easpay.lib/EasypayRequest");
import PaymentResponseNS = require("./easpay.lib/IPaymentResponse");
import StatusResponseNS = require("./easpay.lib/IStatusResponse");
var EasypayRequest = EasypayRequestNS.easypay.EasypayRequest;
var CheckoutPageRequest = CheckoutPageRequestNS.easypay.CheckoutPageRequest;

var errors = {};

errors[404] = (req, res) => {
    var statusCode = 404;
    var result = {
        status: statusCode
    };

    res.status(result.status);
    res.json(result);
};

var config: IEasypayConfig = {
    host: "easypay-staging.swisscom.ch",
    merchantId: "LTC001",
    basepath: "/charging-engine-checkout",
    protocol: "http",
    easypaySecret: "KiLgscVNTqAJQ1keGOv_hhKsuf5oftohg17VmncT"
};

// simple logger for this router's requests
// all requests to this router will first hit this middleware
router.use((req, res, next) => {
    console.log('%s %s %s', req.method, req.url, req.path);
    next();
});

router.get('/', (req, res) => {
    res.render("buy");
});

router.get('/buy', (req, res) => {
    config.basepath = "/charging-engine-checkout";
    var paymentInfo = req.query.paymentInfo;
    var amount = "" + req.query.amount;
    console.log(req.query);

    var baseReturnUrl = "http://" + req.hostname + ":3000/callback";

    var paymentRequest = new CheckoutPageRequest();
    paymentRequest.amount = amount || "4.50";
    paymentRequest.paymentInfo = paymentInfo || "Test";
    paymentRequest.title = "My new Phone";
    paymentRequest.description = "Buy your favorite Sony Phone!";
    paymentRequest.adultContent = false;
    paymentRequest.roaming = false;
    paymentRequest.merchantId = config.merchantId;
    paymentRequest.cancelUrl = baseReturnUrl + "?uid=43c2&sid=dc0d&purchase=cancel";
    paymentRequest.errorUrl = baseReturnUrl + "?uid=43c2&sid=dc0d&purchase=error";
    paymentRequest.successUrl = baseReturnUrl + "?uid=43c2&sid=dc0d&purchase=success";
    paymentRequest.cpServiceId = "xys-323-gh-ff";
    paymentRequest.cpSubscriptionId = "23hkb379oh";
    paymentRequest.cpUserId = "vghv5678";
    paymentRequest.imageUrl = "http://lorempixel.com/300/200";
    paymentRequest.contentType = "App";
    paymentRequest.storeSource = "Easypay-City";

    var url = EasypayRequest.getCheckoutPageUrl(config, paymentRequest);
    return res.redirect(url);
});

router.get('/callback', (req, res) => {
    console.log(req.query);
    var purchase = req.query.purchase;
    var paymentId = req.query.paymentId;
    var msg = { title: "Technical Problem", message: "", paymentId: paymentId, reqId: "" };

    if (purchase === "success") {
        var reqId = msg.reqId = "req-id-123456";
        config.basepath = "/ce-rest-service";
        EasypayRequest.commitPaymentRequest(paymentId, config, reqId).then(() => {
            msg.title = "Success";
            res.render("result", msg);
        }).catch((message: PaymentResponseNS.IPaymentResponse) => {
            msg.title = "Error";
            msg.message = message.messages.map(x=> x.field + ", " + x.message + ", " + x.code).join(" / ");
            res.render("result", msg);
        });
    }
    else {
        msg.title = "Purchase terminated with state: '" + purchase + "'";
        res.render("result", msg);
    }
});

router.get('/status', (req, res) => {
    console.log(req.query);
    var reqId = req.query.reqId;
    var paymentId = req.query.paymentId;
    config.basepath = "/ce-rest-service";

    EasypayRequest.getPaymentStatus(paymentId, config, reqId).then((result: StatusResponseNS.IStatusResponse) => {
        var msg = { title: "STATUS: " + result.status, message: `Msisdn: ${result.formattedMsisdn}, OrderID: ${result.orderID}, Payment Info: ${result.paymentInfo}, CHF ${result.amount}`, paymentId: paymentId, reqId: reqId };
        res.render("result", msg);
    }).catch((msg) => {
        res.render("result", { title: "Error", message: msg.error });
    });
});

// All undefined routes should return a 404
router.use(errors[404]);
export = router;
