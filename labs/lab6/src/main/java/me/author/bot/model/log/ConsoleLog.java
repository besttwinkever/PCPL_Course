package me.author.bot.model.log;

public class ConsoleLog implements ILog {

    @Override
    public void Log(String prefix, String text) {
        System.out.printf("[%s]: %s%n", prefix, text);
    }
}
