package com.swisscom.easypay.easypayclient;

import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.swisscom.easypay.clientlibrary.EasypayConfig;
import com.swisscom.easypay.clientlibrary.EasypayRequest;
import com.swisscom.easypay.clientlibrary.StatusResponse;

import java.util.concurrent.ExecutionException;

public class ResultActivity extends AppCompatActivity {

    public static final String TITLE = "TITLE";
    public static final String MESSAGE = "MESSAGE";
    public static final String PAYMENT_ID = "PAYMENT_ID";
    public static final String REQ_ID = "REQ_ID";
    private String paymentId;
    private String reqId;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_result);

        Intent intent = getIntent();
        String title = intent.getStringExtra(TITLE);
        String message = intent.getStringExtra(MESSAGE);
        paymentId = intent.getStringExtra(PAYMENT_ID);
        reqId = intent.getStringExtra(REQ_ID);

        TextView txtMessage = (TextView) findViewById(R.id.txtMessage);
        txtMessage.setText(message);
        TextView txtTitle = (TextView) findViewById(R.id.txtTitle);
        txtTitle.setText(title);

        Button btnStatus = (Button) findViewById(R.id.btnStatus);

        btnStatus.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                GetPaymentStatusRequestTask task = new GetPaymentStatusRequestTask();
                AsyncTask<String, Void, StatusResponse> execute = task.execute(paymentId, reqId);

                StatusResponse response = new StatusResponse();
                try {
                    response = execute.get();
                } catch (InterruptedException | ExecutionException e) {
                    e.printStackTrace();
                }

                String title = "STATUS: " + response.getStatus();
                String message = "MSISDN: " + response.getFormattedMsisdn() + ", OrderID: " + response.getOrderID() + ", Payment Info: " + response.getPaymentInfo() + ", CHF " + response.getAmount();

                TextView txtMessage = (TextView) findViewById(R.id.txtMessage);
                txtMessage.setText(message);
                TextView txtTitle = (TextView) findViewById(R.id.txtTitle);
                txtTitle.setText(title);
            }
        });
    }

    class GetPaymentStatusRequestTask extends AsyncTask<String, Void, StatusResponse> {

        protected StatusResponse doInBackground(String... args) {
            try {
                EasypayConfig config = DemoActivity.getEasypayConfig();
                config.setBasepath("/ce-rest-service");

                String paymentId = args[0];
                String reqId = args[1];

                return EasypayRequest.GetPaymentStatus(paymentId, config, reqId);
            } catch (Exception e) {
                return null;
            }
        }
    }

}
