package me.author.bot.model;

import com.pengrad.telegrambot.TelegramBot;
import com.pengrad.telegrambot.UpdatesListener;
import com.pengrad.telegrambot.model.Message;
import com.pengrad.telegrambot.model.Update;
import com.pengrad.telegrambot.model.request.ReplyKeyboardMarkup;
import com.pengrad.telegrambot.request.SendMessage;
import me.author.bot.Main;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class Bot {

    // make private add getter/setter
    private TelegramBot telegramBot;
    private String token;

    public Bot(String token) {
        telegramBot = new TelegramBot(token);
        this.token = token;

        telegramBot.setUpdatesListener(new BotListener());

    }

    public TelegramBot getTelegramBot() {
        return telegramBot;
    }

    public String getToken() {
        return token;
    }

    public void sendMessage(long userId, String message) {
        sendMessage(userId, message, new ArrayList<String>());
    }

    public void sendMessage(long userId, String message, List<String> buttons) {
        SendMessage request = new SendMessage(
                userId,
                message
        );
        ReplyKeyboardMarkup keyboard = null;
        if (buttons.size() > 0) {
            keyboard = new ReplyKeyboardMarkup(buttons.get(0));
            for (int i = 1; i < buttons.size(); i++) {
                keyboard.addRow(buttons.get(i));
            }
        }
        request.replyMarkup(keyboard);
        telegramBot.execute(request);
    }

    private class BotListener implements UpdatesListener {

        @Override
        public int process(List<Update> list) {

            for (Update update : list) {

                Message message = update.message();
                if (message != null) {
                    String text = message.text();
                    switch (text.toLowerCase()) {
                        case "проверочная кнопка": {
                            sendMessage(message.chat().id(), "Все работает!", Main.buttons);
                            return CONFIRMED_UPDATES_ALL;
                        }
                        case "текущая дата": {
                            sendMessage(message.chat().id(), String.format("Текущая дата: %s", new SimpleDateFormat("dd/MM/yyyy").format(new Date())), Main.buttons);
                            return CONFIRMED_UPDATES_ALL;
                        }
                    }
                    sendMessage(message.chat().id(), "Неизвестная операция :с", Main.buttons);
                }

            }

            return CONFIRMED_UPDATES_ALL;

        }
    }

}
