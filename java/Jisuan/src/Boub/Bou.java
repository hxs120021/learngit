package Boub;
import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.util.*;

/**
 * Created by 战地记者 on 2016/6/4.
 */
public class Bou extends JFrame {
    JSplitPane jSplitPane;
    JPanel jPanel1, jPanel2;
    JTextField[] jTextField = new JTextField[3];
    JLabel[] jLabel = new JLabel[3];
    JButton[] jButtons = new JButton[18];
    String now1 = new String();//总算式
    String now2 = new String();//当前数字
    ArrayList<Float> arrayListFloat = new ArrayList<Float>();
    ArrayList<String> arrayListString = new ArrayList<String>();
    Actionev actionev = new Actionev();

    public Bou(String title) {
        setTitle(title);
        setVisible(true);
        setBounds(100, 100, 250, 500);
        AddDouble();
        AddTextField();
        AddButtons(jPanel2);
        validate();
        AddListen();
        this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    }

    private void AddDouble() {
        jPanel1 = new JPanel();
        jPanel2 = new JPanel();
        jSplitPane = new JSplitPane(JSplitPane.VERTICAL_SPLIT, jPanel1, jPanel2);
        jSplitPane.setDividerLocation(160);
        jSplitPane.setVisible(true);
        this.add(jSplitPane);//貌似总是忘了这一句。。。
    }

    private void AddFlow(JPanel jPanel) {
        FlowLayout flowLayout = new FlowLayout();
        flowLayout.setAlignment(FlowLayout.LEFT);
        flowLayout.setVgap(5);
        flowLayout.setHgap(6);
        jPanel.setLayout(flowLayout);
    }

    private void AddTextField() {
        String[] project = new String[]{"当前运算数", "当前算式", "结果"};
        for (int i = 0; i < 3; i++) {
            jLabel[i] = new JLabel(project[i]);
            jTextField[i] = new JTextField(20);
            jPanel1.add(jLabel[i]);
            jPanel1.add(jTextField[i]);
        }
    }

    private void AddButtons(JPanel jPanel) {
        String[] strings = new String[]
                {"C", "←", "÷", "7", "8", "9", "×", "4", "5", "6", "-", "1", "2", "3", "+", "0", ".", "="};
        AddFlow(jPanel);
        for (int i = 0; i < 18; i++) {
            jButtons[i] = new JButton(strings[i]);
            jPanel.add(jButtons[i]);
            if (i == 0 || i == 15)
                jButtons[i].setPreferredSize(new Dimension(105, 50));
            else
                jButtons[i].setPreferredSize(new Dimension(50, 50));
        }
    }

    private void AddListen() {
        for (int i = 0; i < 18; i++) {
            if (i == 0)
                jButtons[i].addActionListener(a5);
            else if (i == 1)
                jButtons[i].addActionListener(a3);
            else if (i == 2 || i == 6 || i == 10 || i == 14)
                jButtons[i].addActionListener(a2);
            else if (i == 17)
                jButtons[i].addActionListener(a4);
            else
                jButtons[i].addActionListener(a1);

        }
    }

    ActionListener a1 = new ActionListener() {
        //数字键
        @Override
        public void actionPerformed(ActionEvent e) {
            if (e.getActionCommand() == ".") {
                if (jTextField[0].getText().indexOf(".") == -1) {
                    now1 += e.getActionCommand();
                    now2 += e.getActionCommand();
                    jTextField[0].setText(now2);
                }//不存在"."
            } else if (e.getActionCommand() == "0" && jTextField[0].getText().length() == 1) {

            } else {

                now1 += e.getActionCommand();
                now2 += e.getActionCommand();
                jTextField[0].setText(now2);
            }
        }
    };
    ActionListener a2 = new ActionListener() {
        //运算符键
        @Override
        public void actionPerformed(ActionEvent e) {
            String str = e.getActionCommand();
            if (now2.length() == 0) {
                if (now1.length() == 0) {
                    now1 += "0";
                    now2 += "0";
                }
            }
            if (now2.length() == 0 && now1.length() != 0) {
                now1 = now1.substring(0, now1.length() - 1);
                now1 += str;
                arrayListString.set(arrayListFloat.size() - 1, str);
                jTextField[1].setText(now1);
            } else {
                arrayListFloat.add(Float.parseFloat(now2));
                now2 = "";
                jTextField[0].setText(now2);
                now1 += str;
                arrayListString.add(str);
                jTextField[1].setText(now1);
            }
        }
    };
    ActionListener a3 = new ActionListener() {
        //退格
        @Override
        public void actionPerformed(ActionEvent e) {
            if (now2.length() > 0) {
                now2 = now2.substring(0, now2.length() - 1);
                now1 = now1.substring(0, now1.length() - 1);
                jTextField[0].setText(now2);
            } else {
                now2 = new String();
            }
        }

    };
    ActionListener a4 = new ActionListener() {
        //=
        @Override
        public void actionPerformed(ActionEvent e) {
            try {
                Actionev actionev1;
                if (now2.length() != 0) {
                    arrayListFloat.add(Float.parseFloat(now2));
                    now2 = "";
                    jTextField[0].setText(now2);
                    now1 += e.getActionCommand();
                    arrayListString.add(e.getActionCommand());
                    actionev = new Actionev(arrayListFloat, arrayListString);
                    now1 = "";
                    jTextField[1].setText(now1);
                    jTextField[2].setText("" + actionev.XResult());
                } else {
                    String s = arrayListString.get(arrayListString.size() - 1);
                    arrayListString.set(arrayListFloat.size() - 1, "=");
                    actionev1 = new Actionev(arrayListFloat, arrayListString);
                    float f = actionev1.XResult();
                    arrayListFloat.clear();
                    for (int i = 0; i < 2; i++) {
                        arrayListFloat.add(f);
                    }
                    arrayListString.set(0, s);
                    arrayListString.add("=");
                    actionev1 = new Actionev(arrayListFloat, arrayListString);
                    jTextField[2].setText("" + actionev1.XResult());
                }
                arrayListString.clear();
                arrayListFloat.clear();
            } catch (Exception e1) {
                arrayListString.clear();
                arrayListFloat.clear();
                now1 = "";
                now2 = "";
                jTextField[2].setText("null");
            }
        }

    };
        ActionListener a5 = new ActionListener() {
            //清空
            @Override
            public void actionPerformed(ActionEvent e) {
                arrayListFloat.clear();
                arrayListString.clear();
                now1 = "";
                now2 = "";
                for (int i = 0; i < 3; i++) {
                    jTextField[i].setText("");
                }
            }
        };
    }

