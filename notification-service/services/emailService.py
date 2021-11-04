import json
import requests

email_template = '<!DOCTYPE html><html lang="en" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:v="urn:schemas-microsoft-com:vml"><head><title></title><meta charset="utf-8"/><meta content="width=device-width, initial-scale=1.0" name="viewport"/><link href="https://fonts.googleapis.com/css?family=Open+Sans" rel="stylesheet" type="text/css"/><style>* {box-sizing: border-box;}body {margin: 0;padding: 0;}th.column {padding: 0}a[x-apple-data-detectors] {color: inherit !important;text-decoration: inherit !important;}#MessageViewBody a {color: inherit;text-decoration: none;}p {line-height: inherit}@media (max-width:520px) {.icons-inner {text-align: center;}.icons-inner td {margin: 0 auto;}.row-content {width: 100% !important;}.stack .column {width: 100%;display: block;}}</style></head><body style="background-color: #FFFFFF; margin: 0; padding: 0; -webkit-text-size-adjust: none; text-size-adjust: none;"><table border="0" cellpadding="0" cellspacing="0" class="nl-container" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #FFFFFF;" width="100%"><tbody><tr><td><table align="center" border="0" cellpadding="0" cellspacing="0" class="row row-1" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt;" width="100%"><tbody><tr><td><table align="center" border="0" cellpadding="0" cellspacing="0" class="row-content stack" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt; color: #000000;" width="500"><tbody><tr><th class="column" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;" width="100%"><table border="0" cellpadding="35" cellspacing="0" class="text_block" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;" width="100%"><tr><td><div style="font-family: Arial, sans-serif"><div style="font-size: 14px; font-family: "Open Sans", "Helvetica Neue", Helvetica, Arial, sans-serif; mso-line-height-alt: 16.8px; color: #555555; line-height: 1.2;"><p style="margin: 0; font-size: 14px; text-align: center;"><span style="font-size:42px;"><strong>Just Trade It</strong></span></p></div></div></td></tr></table><table border="0" cellpadding="10" cellspacing="0" class="divider_block" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt;" width="100%"><tr><td><div align="center"><table border="0" cellpadding="0" cellspacing="0" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt;" width="100%"><tr><td class="divider_inner" style="font-size: 1px; line-height: 1px; border-top: 1px solid #BBBBBB;"><span> </span></td></tr></table></div></td></tr></table><table border="0" cellpadding="10" cellspacing="0" class="text_block" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt; word-break: break-word;" width="100%"><tr><td><div style="font-family: Arial, sans-serif"><div style="font-size: 12px; font-family: "Open Sans", "Helvetica Neue", Helvetica, Arial, sans-serif; mso-line-height-alt: 14.399999999999999px; color: #555555; line-height: 1.2;"><p style="margin: 0; font-size: 12px; text-align: center;"><span style="font-size:22px;">Dear Trader</span></p><p style="margin: 0; font-size: 12px; text-align: center;"><span style="font-size:22px;"> you received a new trade :)</span></p><p style="margin: 0; font-size: 12px; text-align: center;"><span style="font-size:22px;"> go to www.justtradeit.com </span></p><p style="margin: 0; font-size: 12px; text-align: center;"><span style="font-size:22px;">to see your trade</span><br/><br/><br/><br/><br/></p></div></div></td></tr></table><table border="0" cellpadding="0" cellspacing="0" class="addon_block" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt;" width="100%"><tr><td style="width:100%;padding-right:0px;padding-left:0px;"><div align="center" style="line-height:10px">%s<img alt="Image" src="https://media1.giphy.com/media/DhstvI3zZ598Nb1rFf/giphy.gif?cid=20eb4e9dachohoa1ruj5lvb8a58qd4ltth2u1s1lnz78y4f5&amp;rid=giphy.gif&amp;ct=g" style="display: block; height: auto; width: 480px; max-width: 100%;" title="Image" width="480"/></div></td></tr></table></th></tr></tbody></table></td></tr></tbody></table><table align="center" border="0" cellpadding="0" cellspacing="0" class="row row-2" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt;" width="100%"><tbody><tr><td><table align="center" border="0" cellpadding="0" cellspacing="0" class="row-content stack" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt; color: #000000;" width="500"><tbody><tr><th class="column" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt; font-weight: 400; text-align: left; vertical-align: top; padding-top: 5px; padding-bottom: 5px; border-top: 0px; border-right: 0px; border-bottom: 0px; border-left: 0px;" width="100%"><table border="0" cellpadding="0" cellspacing="0" class="icons_block" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt;" width="100%"><tr><td style="color:#9d9d9d;font-family:inherit;font-size:15px;padding-bottom:5px;padding-top:5px;text-align:center;"><table cellpadding="0" cellspacing="0" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt;" width="100%"><tr><td style="text-align:center;"><table cellpadding="0" cellspacing="0" class="icons-inner" role="presentation" style="mso-table-lspace: 0pt; mso-table-rspace: 0pt; display: inline-block; margin-right: -4px; padding-left: 0px; padding-right: 0px;"><tr></tr></table></td></tr></table></td></tr></table></th></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></body></html>'


def send_simple_message(to, subject, body):
    return requests.post(
        "https://api.mailgun.net/v3/sandbox39f0513cae5c403392c92c8229289342.mailgun.org/messages",
        auth=("api", "f623bcbd0ec833f0552f28201735adf9-2ac825a1-8caad992"),
        data={"from": "Mailgun Sandbox <postmaster@sandbox39f0513cae5c403392c92c8229289342.mailgun.org>",
              "to": to,
              "subject": subject,
              "html": body})

def send_trade_email(ch, method, properties, data):
    parsed_msg = json.loads(data)
    email = parsed_msg['email']
    print(email)
    representation = email_template
    send_simple_message(parsed_msg['email'], 'You Just Received a new Trade!', representation)


def send_trade_update_email(ch, method, properties, data):
    parsed_msg = json.loads(data)
    print(parsed_msg)
    receiver = parsed_msg["Receiver"]
    rec_email = receiver["Email"]
    rec_items = parsed_msg["ReceivingItems"]
    send = parsed_msg["Sender"]
    off_items = parsed_msg["OfferingItems"]
    rec_item_html = ''.join([ '<b><p>Title: %s</p></b><p style=line-height:1.6>Description: %s</p>' % (item["Title"], item["ShortDescription"])for item in rec_items])
    off_item_html = ''.join([ '<b><p>Title: %s</p></b><p style=line-height:1.6>Description: %s</p>' % (item["Title"], item["ShortDescription"])for item in off_items])  
    email_template = f"""<!DOCTYPE html>
    <html>
    <head>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Roboto+Mono:ital,wght@0,400;1,100;1,200&display=swap" rel="stylesheet">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <style>
        div {{
            margin: 10px;
        }}
        .first {{
            width: 25%;
            display: inline-block;
            background-color: lightgray;
            float: left;
            overflow-wrap: break-word;
            padding: 10px;
            font-family: 'Roboto Mono', monospace;
        }}
        .second {{
            width: 25%;
            display: inline-block;
            background-color: lightgray;
            float: left;
            overflow-wrap: break-word;
            padding: 10px;
            font-family: 'Roboto Mono', monospace;
            
        }}
        .third {{
            width: 25%;
            display: inline-block;
            background-color: lightgray;
            float: left;
            overflow-wrap: break-word;
            padding: 10px;
            font-family: 'Roboto Mono', monospace;  
        }}
        .body{{
            font-family: 'Roboto Mono', monospace;
        }}
        hr{{
        border: 0;
        height: 0.5px;
        background-image: linear-gradient(to right, #bfbfbf, #100,#bfbfbf);
    }}


            @media screen and (max-width: 900px) {{
                .first,
                .second,
                .third {{
                    width: 70%;
                }}
            }}
        </style>
    </head>

    <body>
        <h1>Your Trade Has Been Updated</h1>
        <p>Your trade has been {parsed_msg["Status"]}</p>
        <div class="first">
            <h3>Trade Information</h3>
            <hr>
            <p>Your Email: {receiver["Email"]}</p>
            <p>Issued Date: {parsed_msg["IssuedDate"]}</p>
            <p>Modified By: {parsed_msg["ModifiedBy"]}</p>
            <p>Trade Status: {parsed_msg["Status"]}</p>
            <b><p>Sender</p></b>
            <p>Name: {send["FullName"]}</p>
            <p>Email: {send["Email"]}</p>
            <p>Profile Image</p>
            <img src={send["ProfileImageUrl"]} width="auto" height="250px">
        </div>
        <div class="second">
            <h3  style=text-align:center>Receiving Items</h2>
            <hr>
            {rec_item_html}
        </div>
        <div class="third">
            <h3 style=text-align:center>Offering Items</h2>
            <hr>
             {off_item_html}
        </div>

    </body>
    </html>"""
    send_simple_message(rec_email, 'Your trade has been updated', email_template)


def send(ch, method, properties, data):
    print('test')

