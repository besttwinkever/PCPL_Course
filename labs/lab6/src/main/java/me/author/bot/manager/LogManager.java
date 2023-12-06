package me.author.bot.manager;

import me.author.bot.model.log.ConsoleLog;
import me.author.bot.model.log.ILog;

import java.util.ArrayList;
import java.util.List;

public class LogManager {

    private static final List<ILog> logs = new ArrayList<>();

    public static void init() {
        logs.add(new ConsoleLog());
    }

    public static void log(String prefix, String message) {
        for (ILog log : logs)
            log.Log(prefix, message);
    }

    public static void log(String message) {
        for (ILog log : logs)
            log.Log("INFO", message);
    }

}
