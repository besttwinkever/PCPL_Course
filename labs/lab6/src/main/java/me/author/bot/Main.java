package me.author.bot;

import me.author.bot.manager.BotManager;
import me.author.bot.manager.LogManager;
import me.author.bot.model.Bot;

import java.util.ArrayList;
import java.util.List;

public class Main {

    private static final String token = "";
    private static final long ownerId = 1;
    public static List<String> buttons = new ArrayList<>();

    public static void main(String[] args) {

        buttons.add("Проверочная кнопка");
        buttons.add("Текущая дата");
        LogManager.init();

        LogManager.log("Инициализируем бота...");
        Bot bot = BotManager.registerBot(token);

        LogManager.log("Отправляем тестовое сообщение...");
        bot.sendMessage(ownerId, "Я запустился", buttons);

    }

}
