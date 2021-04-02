public class addrMapDes {
        public static String mapping(int mode, int addr) {
            String rst = "";
            if (mode == 1) {
                switch (addr) {
                case 400001:
                    rst = "기관코드";
                    break;
                case 400002:
                    rst = "회사코드";
                    break;
                case 400003:
                    rst = "제품타입";
                    break;
                case 400004:
                    rst = "제품코드";
                    break;
                case 400005:
                    rst = "프로토콜버전";
                    break;
                case 400006:
                    rst = "채널수";
                    break;
                case 400007:
                    rst = "노드 시리얼번호";
                    break;
                case 400008:
                    rst = "노드 시리얼번호";
                    break;
                case 400101:
                    rst = "온도1";
                    break;
                case 400102:
                    rst = "온도2";
                    break;
                case 400103:
                    rst = "온도3";
                    break;
                case 400104:
                    rst = "습도1";
                    break;
                case 400105:
                    rst = "이슬점센서";
                    break;
                case 400106:
                    rst = "감우센서";
                    break;
                case 400107:
                    rst = "유량센서";
                    break;
                case 400108:
                    rst = "강우센서";
                    break;
                case 400109:
                    rst = "일사센서";
                    break;
                case 400110:
                    rst = "풍속센서";
                    break;
                case 400111:
                    rst = "풍향센서";
                    break;
                case 400112:
                    rst = "전압센서";
                    break;
                case 400113:
                    rst = "CO2센서";
                    break;
                case 400114:
                    rst = "EC센서";
                    break;
                case 400115:
                    rst = "광양자센서";
                    break;
                case 400116:
                    rst = "토양함수율센서";
                    break;
                case 400117:
                    rst = "토양수분장력센서";
                    break;
                case 400118:
                    rst = "pH";
                    break;
                case 400119:
                    rst = "지온";
                    break;
                case 400120:
                    rst = "온도4";
                    break;
                case 400121:
                    rst = "온도5";
                    break;
                case 400122:
                    rst = "온도6";
                    break;
                case 400123:
                    rst = "온도7";
                    break;
                case 400124:
                    rst = "온도8";
                    break;
                case 400125:
                    rst = "온도9";
                    break;
                case 400126:
                    rst = "온도10";
                    break;
                case 400127:
                    rst = "습도2";
                    break;
                case 400128:
                    rst = "습도3";
                    break;
                case 400129:
                    rst = "무게1";
                    break;
                case 400130:
                    rst = "무게2";
                    break;
                case 400202:
                    rst = "노드 상태";
                    break;
                case 400203:
                case 400204:
                    rst = "온도센서(#1) 값";
                    break;
                case 400205:
                    rst = "온도센서(#1) 상태";
                    break;
                case 400206:
                case 400207:
                    rst = "온도센서(#2) 값";
                    break;
                case 400208:
                    rst = "온도센서(#2) 상태";
                    break;
                case 400209:
                case 400210:
                    rst = "온도센서(#3) 값";
                    break;
                case 400211:
                    rst = "온도센서(#3) 상태";
                    break;
                case 400212:
                case 400213:
                    rst = "습도센서(#1) 값";
                    break;
                case 400214:
                    rst = "습도센서(#1) 상태";
                    break;
                case 400215:
                case 400216:
                    rst = "이슬점센서 값";
                    break;
                case 400217:
                    rst = "이슬점센서 상태";
                    break;
                case 400218:
                case 400219:
                    rst = "감우센서 값";
                    break;
                case 400220:
                    rst = "감우센서 상태";
                    break;
                case 400221:
                case 400222:
                    rst = "유량센서 값";
                    break;
                case 400223:
                    rst = "유량센서 상태";
                    break;
                case 400224:
                case 400225:
                    rst = "강우센서 값";
                    break;
                case 400226:
                    rst = "강우센서 상태";
                    break;
                case 400227:
                case 400228:
                    rst = "일사센서 값";
                    break;
                case 400229:
                    rst = "일사센서 상태";
                    break;
                case 400230:
                case 400231:
                    rst = "풍속센서 값";
                    break;
                case 400232:
                    rst = "풍속센서 상태";
                    break;
                case 400233:
                case 400234:
                    rst = "풍향센서 값";
                    break;
                case 400235:
                    rst = "풍향센서 상태";
                    break;
                case 400236:
                case 400237:
                    rst = "전압센서 값";
                    break;
                case 400238:
                    rst = "전압센서 상태";
                    break;
                case 400239:
                case 400240:
                    rst = "CO2센서 값";
                    break;
                case 400241:
                    rst = "CO2센서 상태";
                    break;
                case 400242:
                case 400243:
                    rst = "EC센서 값";
                    break;
                case 400244:
                    rst = "EC센서 상태";
                    break;
                case 400245:
                case 400246:
                    rst = "광양자센서 값";
                    break;
                case 400247:
                    rst = "광양자센서 상태";
                    break;
                case 400248:
                case 400249:
                    rst = "토양함수율센서 값";
                    break;
                case 400250:
                    rst = "토양함수율센서 상태";
                    break;
                case 400251:
                case 400252:
                    rst = "토양수분장력센서 값";
                    break;
                case 400253:
                    rst = "토양수분장력센서 상태";
                    break;
                case 400254:
                case 400255:
                    rst = "pH센서 값";
                    break;
                case 400256:
                    rst = "pH센서 상태";
                    break;
                case 400257:
                case 400258:
                    rst = "지온센서 값";
                    break;
                case 400259:
                    rst = "지온센서 상태";
                    break;
                case 400260:
                case 400261:
                    rst = "온도#4";
                    break;
                case 400262:
                    rst = "온도센서 상태";
                    break;
                case 400263:
                case 400264:
                    rst = "온도#5";
                    break;
                case 400265:
                    rst = "온도센서 상태";
                    break;
                case 400266:
                case 400267:
                    rst = "온도#6";
                    break;
                case 400268:
                    rst = "온도센서 상태";
                    break;
                case 400269:
                case 400270:
                    rst = "온도#7";
                    break;
                case 400271:
                    rst = "온도센서 상태";
                    break;
                case 400272:
                case 400273:
                    rst = "온도#8";
                    break;
                case 400274:
                    rst = "온도센서 상태";
                    break;
                case 400275:
                case 400276:
                    rst = "온도#9";
                    break;
                case 400277:
                    rst = "온도센서 상태";
                    break;
                case 400278:
                case 400279:
                    rst = "온도#10";
                    break;
                case 400280:
                    rst = "온도센서 상태";
                    break;
                case 400281:
                case 400282:
                    rst = "습도#2";
                    break;
                case 400283:
                    rst = "습도센서 상태";
                    break;
                case 400284:
                case 400285:
                    rst = "습도#3";
                    break;
                case 400286:
                    rst = "습도센서 상태";
                    break;
                case 400287:
                case 400288:
                    rst = "무게#1";
                    break;
                case 400289:
                    rst = "무게센서 상태";
                    break;
                case 400290:
                case 400291:
                    rst = "무게#2";
                    break;
                case 400292:
                    rst = "무게센서 상태";
                    break;
                }
            } else if (mode == 2) {
                switch (addr) {
                case 400001:
                    rst = "스위치 #1";
                    break;
                case 400002:
                    rst = "스위치 #2";
                    break;
                case 400003:
                    rst = "스위치 #3";
                    break;
                case 400004:
                    rst = "스위치 #4";
                    break;
                case 400005:
                    rst = "스위치 #5";
                    break;
                case 400006:
                    rst = "스위치 #6";
                    break;
                case 400007:
                    rst = "스위치 #7";
                    break;
                case 400008:
                    rst = "스위치 #8";
                    break;
                case 400009:
                    rst = "스위치 #9";
                    break;
                case 400010:
                    rst = "스위치 #10";
                    break;
                case 400011:
                    rst = "스위치 #11";
                    break;
                case 400012:
                    rst = "스위치 #12";
                    break;
                case 400013:
                    rst = "스위치 #13";
                    break;
                case 400014:
                    rst = "스위치 #14";
                    break;
                case 400015:
                    rst = "스위치 #15";
                    break;
                case 400016:
                    rst = "스위치 #16";
                    break;
                case 400017:
                    rst = "개폐기 #1";
                    break;
                case 400018:
                    rst = "개폐기 #2";
                    break;
                case 400019:
                    rst = "개폐기 #3";
                    break;
                case 400020:
                    rst = "개폐기 #4";
                    break;
                case 400021:
                    rst = "개폐기 #5";
                    break;
                case 400022:
                    rst = "개폐기 #6";
                    break;
                case 400023:
                    rst = "개폐기 #7";
                    break;
                case 400024:
                    rst = "개폐기 #8";
                    break;
                case 400201:
                    rst = "노드 OPID";
                    break;
                case 400202:
                    rst = "노드 상태";
                    break;
                case 400203:
                    rst = "스위치1 OPID";
                    break;
                case 400204:
                    rst = "스위치1 상태";
                    break;
                case 400205:
                case 400206:
                    rst = "스위치1 남은동작시간";
                    break;
                case 400207:
                    rst = "스위치2 OPID";
                    break;
                case 400208:
                    rst = "스위치2 상태";
                    break;
                case 400209:
                case 400210:
                    rst = "스위치2 남은동작시간";
                    break;
                case 400211:
                    rst = "스위치3 OPID";
                    break;
                case 400212:
                    rst = "스위치3 상태";
                    break;
                case 400213:
                case 400214:
                    rst = "스위치3 남은동작시간";
                    break;
                case 400215:
                    rst = "스위치4 OPID";
                    break;
                case 400216:
                    rst = "스위치4 상태";
                    break;
                case 400217:
                case 400218:
                    rst = "스위치4 남은동작시간";
                    break;
                case 400219:
                    rst = "스위치5 OPID";
                    break;
                case 400220:
                    rst = "스위치5 상태";
                    break;
                case 400221:
                case 400222:
                    rst = "스위치5 남은동작시간";
                    break;
                case 400223:
                    rst = "스위치6 OPID";
                    break;
                case 400224:
                    rst = "스위치6 상태";
                    break;
                case 400225:
                case 400226:
                    rst = "스위치6 남은동작시간";
                    break;
                case 400227:
                    rst = "스위치7 OPID";
                    break;
                case 400228:
                    rst = "스위치7 상태";
                    break;
                case 400229:
                case 400230:
                    rst = "스위치7 남은동작시간";
                    break;
                case 400231:
                    rst = "스위치8 OPID";
                case 400232:
                    rst = "스위치8 상태";
                    break;
                case 400233:
                case 400234:
                    rst = "스위치8 남은동작시간";
                    break;
                case 400235:
                    rst = "스위치9 OPID";
                    break;
                case 400236:
                    rst = "스위치9 상태";
                    break;
                case 400237:
                case 400238:
                    rst = "스위치9 남은동작시간";
                    break;
                case 400239:
                    rst = "스위치10 OPID";
                    break;
                case 400240:
                    rst = "스위치10 상태";
                    break;
                case 400241:
                case 400242:
                    rst = "스위치10 남은동작시간";
                    break;
                case 400243:
                    rst = "스위치11 OPID";
                    break;
                case 400244:
                    rst = "스위치11 상태";
                    break;
                case 400245:
                case 400246:
                    rst = "스위치11 남은동작시간";
                    break;
                case 400247:
                    rst = "스위치12 OPID";
                    break;
                case 400248:
                    rst = "스위치12 상태";
                    break;
                case 400249:
                case 400250:
                    rst = "스위치12 남은동작시간";
                    break;
                case 400251:
                    rst = "스위치13 OPID";
                    break;
                case 400252:
                    rst = "스위치13 상태";
                    break;
                case 400253:
                case 400254:
                    rst = "스위치13 남은동작시간";
                    break;
                case 400255:
                    rst = "스위치14 OPID";
                    break;
                case 400256:
                    rst = "스위치14 상태";
                    break;
                case 400257:
                case 400258:
                    rst = "스위치14 남은동작시간";
                    break;
                case 400259:
                    rst = "스위치15 OPID";
                    break;
                case 400260:
                    rst = "스위치15 상태";
                    break;
                case 400261:
                case 400262:
                    rst = "스위치15 남은동작시간";
                    break;
                case 400263:
                    rst = "스위치16 OPID";
                    break;
                case 400264:
                    rst = "스위치16 상태";
                    break;
                case 400265:
                case 400266:
                    rst = "스위치16 남은동작시간";
                    break;
                case 400267:
                    rst = "OPID #17";
                    break;
                case 400268:
                    rst = "개폐기1 상태";
                    break;
                case 400269:
                case 400270:
                    rst = "개폐기1 남은동작시간";
                    break;
                case 400271:
                    rst = "OPID #18";
                    break;
                case 400272:
                    rst = "개폐기2 상태";
                    break;
                case 400273:
                case 400274:
                    rst = "개폐기2 남은동작시간";
                    break;
                case 400275:
                    rst = "OPID #19";
                    break;
                case 400276:
                    rst = "개폐기3 상태";
                    break;
                case 400277:
                case 400278:
                    rst = "개폐기3 남은동작시간";
                    break;
                case 400279:
                    rst = "OPID #20";
                    break;
                case 400280:
                    rst = "개폐기4 상태";
                    break;
                case 400281:
                case 400282:
                    rst = "개폐기4 남은동작시간";
                    break;
                case 400283:
                    rst = "OPID #21";
                    break;
                case 400284:
                    rst = "개폐기5 상태";
                    break;
                case 400285:
                case 400286:
                    rst = "개폐기5 남은동작시간";
                    break;
                case 400287:
                    rst = "OPID #22";
                    break;
                case 400288:
                    rst = "개폐기6 상태";
                    break;
                case 400289:
                case 400290:
                    rst = "개폐기6 남은동작시간";
                    break;
                case 400291:
                    rst = "OPID #23";
                    break;
                case 400292:
                    rst = "개폐기7 상태";
                    break;
                case 400293:
                case 400294:
                    rst = "개폐기7 남은동작시간";
                    break;
                case 400295:
                    rst = "OPID #24";
                    break;
                case 400296:
                    rst = "개폐기8 상태";
                    break;
                case 400297:
                case 400298:
                    rst = "개폐기8 남은동작시간";
                    break;
                case 400501:
                    rst = "노드명령";
                    break;
                case 400502:
                    rst = "OPID #0";
                    break;
                case 400503:
                    rst = "스위치1 명령";
                    break;
                case 400504:
                    rst = "OPID #1";
                    break;
                case 400505:
                case 400506:
                    rst = "스위치1 동작시간";
                    break;
                case 400507:
                    rst = "스위치2 명령";
                    break;
                case 400508:
                    rst = "OPID #2";
                    break;
                case 400509:
                case 400510:
                    rst = "스위치2 동작시간";
                    break;
                case 400511:
                    rst = "스위치3 명령";
                    break;
                case 400512:
                    rst = "OPID #3";
                    break;
                case 400513:
                case 400514:
                    rst = "스위치3 동작시간";
                    break;
                case 400515:
                    rst = "스위치4 명령";
                    break;
                case 400516:
                    rst = "OPID #4";
                    break;
                case 400517:
                case 400518:
                    rst = "스위치4 동작시간";
                    break;
                case 400519:
                    rst = "스위치5 명령";
                    break;
                case 400520:
                    rst = "OPID #5";
                    break;
                case 400521:
                case 400522:
                    rst = "스위치5 동작시간";
                    break;
                case 400523:
                    rst = "스위치6 명령";
                    break;
                case 400524:
                    rst = "OPID #6";
                    break;
                case 400525:
                case 400526:
                    rst = "스위치6 동작시간";
                    break;
                case 400527:
                    rst = "스위치7 명령";
                    break;
                case 400528:
                    rst = "OPID #7";
                    break;
                case 400529:
                case 400530:
                    rst = "스위치7 동작시간";
                    break;
                case 400531:
                    rst = "스위치8 명령";
                    break;
                case 400532:
                    rst = "OPID #8";
                    break;
                case 400533:
                case 400534:
                    rst = "스위치8 동작시간";
                    break;
                case 400535:
                    rst = "스위치9 명령";
                    break;
                case 400536:
                    rst = "OPID #9";
                    break;
                case 400537:
                case 400538:
                    rst = "스위치9 동작시간";
                    break;
                case 400539:
                    rst = "스위치10 명령";
                    break;
                case 400540:
                    rst = "OPID #10";
                    break;
                case 400541:
                case 400542:
                    rst = "스위치10 동작시간";
                    break;
                case 400543:
                    rst = "스위치11 명령";
                    break;
                case 400544:
                    rst = "OPID #11";
                    break;
                case 400545:
                case 400546:
                    rst = "스위치11 동작시간";
                    break;
                case 400547:
                    rst = "스위치12 명령";
                    break;
                case 400548:
                    rst = "OPID #12";
                    break;
                case 400549:
                case 400550:
                    rst = "스위치12 동작시간";
                    break;
                case 400551:
                    rst = "스위치13 명령";
                    break;
                case 400552:
                    rst = "OPID #13";
                    break;
                case 400553:
                case 400554:
                    rst = "스위치13 동작시간";
                    break;
                case 400555:
                    rst = "스위치14 명령";
                    break;
                case 400556:
                    rst = "OPID #14";
                    break;
                case 400557:
                case 400558:
                    rst = "스위치14 동작시간";
                    break;
                case 400559:
                    rst = "스위치15 명령";
                    break;
                case 400560:
                    rst = "OPID #15";
                    break;
                case 400561:
                case 400562:
                    rst = "스위치15 동작시간";
                    break;
                case 400563:
                    rst = "스위치16 명령";
                    break;
                case 400564:
                    rst = "OPID #16";
                    break;
                case 400565:
                case 400566:
                    rst = "스위치16 동작시간";
                    break;
                case 400567:
                    rst = "개폐기1 명령";
                    break;
                case 400568:
                    rst = "OPID #17";
                    break;
                case 400569:
                case 400570:
                    rst = "개폐기1 동작시간";
                    break;
                case 400571:
                    rst = "개폐기2 명령";
                    break;
                case 400572:
                    rst = "OPID #18";
                    break;
                case 400573:
                case 400574:
                    rst = "개폐기2 동작시간";
                    break;
                case 400575:
                    rst = "개폐기3 명령";
                    break;
                case 400576:
                    rst = "OPID #19";
                    break;
                case 400577:
                case 400578:
                    rst = "개폐기3 동작시간";
                    break;
                case 400579:
                    rst = "개폐기4 명령";
                    break;
                case 400580:
                    rst = "OPID #20";
                    break;
                case 400581:
                case 400582:
                    rst = "개폐기4 동작시간";
                    break;
                case 400583:
                    rst = "개폐기5 명령";
                    break;
                case 400584:
                    rst = "OPID #21";
                    break;
                case 400585:
                case 400586:
                    rst = "개폐기5 동작시간";
                    break;
                case 400587:
                    rst = "개폐기6 명령";
                    break;
                case 400588:
                    rst = "OPID #22";
                    break;
                case 400589:
                case 400590:
                    rst = "개폐기6 동작시간";
                    break;
                case 400591:
                    rst = "개폐기7 명령";
                    break;
                case 400592:
                    rst = "OPID #23";
                    break;
                case 400593:
                case 400594:
                    rst = "개폐기7 동작시간";
                    break;
                case 400595:
                    rst = "개폐기8 명령";
                    break;
                case 400596:
                    rst = "OPID #24";
                    break;
                case 400597:
                case 400598:
                    rst = "개폐기8 동작시간";
                    break;
                }
            } else if (mode == 3) {
                switch (addr) {
                case 400501:
                    rst = "노드명령";
                    break;
                case 400502:
                    rst = "OPID #0";
                    break;
                case 400503:
                    rst = "스위치1 명령";
                    break;
                case 400504:
                    rst = "OPID #1";
                    break;
                case 400505:
                case 400506:
                    rst = "스위치1 동작시간";
                    break;
                case 400507:
                    rst = "스위치2 명령";
                    break;
                case 400508:
                    rst = "OPID #2";
                    break;
                case 400509:
                case 400510:
                    rst = "스위치2 동작시간";
                    break;
                case 400511:
                    rst = "스위치3 명령";
                    break;
                case 400512:
                    rst = "OPID #3";
                    break;
                case 400513:
                case 400514:
                    rst = "스위치3 동작시간";
                    break;
                case 400515:
                    rst = "스위치4 명령";
                    break;
                case 400516:
                    rst = "OPID #4";
                    break;
                case 400517:
                case 400518:
                    rst = "스위치4 동작시간";
                    break;
                case 400519:
                    rst = "스위치5 명령";
                    break;
                case 400520:
                    rst = "OPID #5";
                    break;
                case 400521:
                case 400522:
                    rst = "스위치5 동작시간";
                    break;
                case 400523:
                    rst = "스위치6 명령";
                    break;
                case 400524:
                    rst = "OPID #6";
                    break;
                case 400525:
                case 400526:
                    rst = "스위치6 동작시간";
                    break;
                case 400527:
                    rst = "스위치7 명령";
                    break;
                case 400528:
                    rst = "OPID #7";
                    break;
                case 400529:
                case 400530:
                    rst = "스위치7 동작시간";
                    break;
                case 400531:
                    rst = "스위치8 명령";
                    break;
                case 400532:
                    rst = "OPID #8";
                    break;
                case 400533:
                case 400534:
                    rst = "스위치8 동작시간";
                    break;
                case 400535:
                    rst = "스위치9 명령";
                    break;
                case 400536:
                    rst = "OPID #9";
                    break;
                case 400537:
                case 400538:
                    rst = "스위치9 동작시간";
                    break;
                case 400539:
                    rst = "스위치10 명령";
                    break;
                case 400540:
                    rst = "OPID #10";
                    break;
                case 400541:
                case 400542:
                    rst = "스위치10 동작시간";
                    break;
                case 400543:
                    rst = "스위치11 명령";
                    break;
                case 400544:
                    rst = "OPID #11";
                    break;
                case 400545:
                case 400546:
                    rst = "스위치11 동작시간";
                    break;
                case 400547:
                    rst = "스위치12 명령";
                    break;
                case 400548:
                    rst = "OPID #12";
                    break;
                case 400549:
                case 400550:
                    rst = "스위치12 동작시간";
                    break;
                case 400551:
                    rst = "스위치13 명령";
                    break;
                case 400552:
                    rst = "OPID #13";
                    break;
                case 400553:
                case 400554:
                    rst = "스위치13 동작시간";
                    break;
                case 400555:
                    rst = "스위치14 명령";
                    break;
                case 400556:
                    rst = "OPID #14";
                    break;
                case 400557:
                case 400558:
                    rst = "스위치14 동작시간";
                    break;
                case 400559:
                    rst = "스위치15 명령";
                    break;
                case 400560:
                    rst = "OPID #15";
                    break;
                case 400561:
                case 400562:
                    rst = "스위치15 동작시간";
                    break;
                case 400563:
                    rst = "스위치16 명령";
                    break;
                case 400564:
                    rst = "OPID #16";
                    break;
                case 400565:
                case 400566:
                    rst = "스위치16 동작시간";
                    break;
                case 400567:
                    rst = "개폐기1 명령";
                    break;
                case 400568:
                    rst = "OPID #17";
                    break;
                case 400569:
                case 400570:
                    rst = "개폐기1 동작시간";
                    break;
                case 400571:
                    rst = "개폐기2 명령";
                    break;
                case 400572:
                    rst = "OPID #18";
                    break;
                case 400573:
                case 400574:
                    rst = "개폐기2 동작시간";
                    break;
                case 400575:
                    rst = "개폐기3 명령";
                    break;
                case 400576:
                    rst = "OPID #19";
                    break;
                case 400577:
                case 400578:
                    rst = "개폐기3 동작시간";
                    break;
                case 400579:
                    rst = "개폐기4 명령";
                    break;
                case 400580:
                    rst = "OPID #20";
                    break;
                case 400581:
                case 400582:
                    rst = "개폐기4 동작시간";
                    break;
                case 400583:
                    rst = "개폐기5 명령";
                    break;
                case 400584:
                    rst = "OPID #21";
                    break;
                case 400585:
                case 400586:
                    rst = "개폐기5 동작시간";
                    break;
                case 400587:
                    rst = "개폐기6 명령";
                    break;
                case 400588:
                    rst = "OPID #22";
                    break;
                case 400589:
                case 400590:
                    rst = "개폐기6 동작시간";
                    break;
                case 400591:
                    rst = "개폐기7 명령";
                    break;
                case 400592:
                    rst = "OPID #23";
                    break;
                case 400593:
                case 400594:
                    rst = "개폐기7 동작시간";
                    break;
                case 400595:
                    rst = "개폐기8 명령";
                    break;
                case 400596:
                    rst = "OPID #24";
                    break;
                case 400597:
                case 400598:
                    rst = "개폐기8 동작시간";
                    break;
                }
            }
            return rst;
        }
    }