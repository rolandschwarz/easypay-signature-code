<!DOCTYPE html>
<html>
<body>
<h2>Easypay Demo Client</h2>
<form method="get" action="easypay_controller.php">
    <div>
        <label for="amount">Amount</label>
        <input id="amount" type="number" name="amount" step="0.05"/>
    </div>
    <div>
        <label for="paymentInfo">Info on Payment</label>
        <input id="paymentInfo" type="text" name="paymentInfo"/>
    </div>
    <input type="submit" name="buy" value="Buy"/>
</form>
</body>
</html>
