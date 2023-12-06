package me.author.bot.manager;

import me.author.bot.model.Bot;

import java.util.ArrayList;
import java.util.List;

public class BotManager {

    private static final List<Bot> bots = new ArrayList<>();

    public static Bot registerBot(String token) {
        LogManager.log("Создаем нового бота...");
        Bot bot = new Bot(token);
        bots.add(bot);
        return bot;
    }

}
