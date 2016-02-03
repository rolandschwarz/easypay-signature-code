package com.swisscom.easypay.easypayclient;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

public class MainActivity extends Activity {

    public static final String AMOUNT = "AMOUNT";
    public static final String PAYMENT_INFO ="PAYMENT_INFO";
    private EditText txtPaymentInfo;
    private EditText txtAmount;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        Button buyBtn = (Button) findViewById(R.id.btnBuy);

        txtPaymentInfo = (EditText) findViewById(R.id.txtPaymentInfo);
        txtAmount = (EditText) findViewById(R.id.txtAmount);

        buyBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(MainActivity.this, DemoActivity.class);
                intent.putExtra(PAYMENT_INFO, txtPaymentInfo.getText().toString());
                intent.putExtra(AMOUNT, Double.parseDouble(txtAmount.getText().toString()));
                startActivity(intent);
            }
        });
    }
}
