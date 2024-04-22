<%@ Page Title="" Language="C#" MasterPageFile="~/TemplateMasterLanding.master" AutoEventWireup="true" CodeFile="Landing.aspx.cs" Inherits="Landing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- ======= Hero Section ======= -->
    <section id="hero" class="d-flex flex-column justify-content-end align-items-center">
        <div id="heroCarousel" class="container carousel carousel-fade" data-ride="carousel">

            <!-- Slide 1 -->
            <div class="carousel-item ">
                <div class="carousel-container">
                    <img class="d-block w-100" src="Banner/ban-01.jpg" alt="First slide">
                </div>
            </div>

            <!-- Slide 2 -->
            <div class="carousel-item">
                <div class="carousel-container">
                    <img class="d-block w-100" src="Banner/banner_2.jpeg" alt="Second slide">
                </div>
            </div>
            <%--jkdfjdkf--%>
            <%--test comment--%> 
            <div class="carousel-item">
                <div class="carousel-container">
                    <img class="d-block w-100" src="Banner/ban-05.jpg" alt="Second slide">
                </div>
            </div>

            <!-- Slide 3 -->
            <div class="carousel-item">
                <div class="carousel-container">
                    <img class="d-block w-100" src="Banner/banner_3.jpeg" alt="Third slide">
                </div>
            </div>
            <!-- Slide 4 -->
            <div class="carousel-item">
                <div class="carousel-container">
                    <img class="d-block w-100" src="Banner/banner_5.jpeg" alt="Fifth slide">
                </div>
            </div>
            <!-- Slide 5 -->
            <div class="carousel-item active">
                <div class="carousel-container">
                    <img class="d-block w-100" src="Banner/ban-02.jpg" alt="Sixth slide">
                </div>
            </div>
            <!-- Slide 6 -->
            <div class="carousel-item">
                <div class="carousel-container">
                    <img class="d-block w-100" src="Banner/banner_1.jpg" alt="Fourth slide">
                </div>
            </div>
            <!-- Slide 6 -->
            <div class="carousel-item">
                <div class="carousel-container">
                    <img class="d-block w-100" src="Banner/banner_4.jpeg" alt="Fourth slide">
                </div>
            </div>
            <!-- Slide 6 -->
            <div class="carousel-item">
                <div class="carousel-container">
                    <img class="d-block w-100" src="Banner/ban-01-nw.jpeg" alt="Fourth slide">
                </div>
            </div>
            <!-- Slide 6 -->
            <div class="carousel-item">
                <div class="carousel-container">
                    <img class="d-block w-100" src="Banner/ban-01-nw.jpeg" alt="Fourth slide">
                </div>
            </div>
            <%--test commetn2--%> 

            <a class="carousel-control-prev" href="#heroCarousel" role="button" data-slide="prev">
                <span class="carousel-control-prev-icon bx bx-chevron-left" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>

            <a class="carousel-control-next" href="#heroCarousel" role="button" data-slide="next">
                <span class="carousel-control-next-icon bx bx-chevron-right" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
            </a>

        </div>

        <svg class="hero-waves" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 24 150 28 " preserveAspectRatio="none">
            <defs>
                <path id="wave-path" d="M-160 44c30 0 58-18 88-18s 58 18 88 18 58-18 88-18 58 18 88 18 v44h-352z">
            </defs>
            <g class="wave1">
                <use xlink:href="#wave-path" x="50" y="3" fill="rgba(255,255,255, .1)">
            </g>
            <g class="wave2">
                <use xlink:href="#wave-path" x="50" y="0" fill="rgba(255,255,255, .2)">
            </g>
            <g class="wave3">
                <use xlink:href="#wave-path" x="50" y="9" fill="#fff">
            </g>
        </svg>

    </section>
    <!-- End Hero -->

    <main id="main">
        <section id="team" class="team">
            <div class="container">

                <div class="section-title aos-init aos-animate" data-aos="zoom-out">
                    <p>Our Dignitaries</p>

                </div>

                <div class="row">



                    <div class="col-lg-3 col-md-6 d-flex align-items-stretch">
                        <div class="member aos-init aos-animate" data-aos="fade-up" data-aos-delay="300">
                            <div class="member-img">
                                <img src="Banner/IMG-CM-1.jpg" class="img-fluid" alt="">
                                <div class="social">
                                </div>
                            </div>
                            <div class="member-info">
                                <h4>योगी आदित्यनाथ</h4>
                                <span>माननीय मुख्यमंत्री
                                (उत्तर प्रदेश)
                                </span>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-3 col-md-6 d-flex align-items-stretch">
                        <div class="member aos-init aos-animate" data-aos="fade-up" data-aos-delay="200">
                            <div class="member-img">
                                <img src="Banner/IMG-Minister-04.jpg" class="img-fluid" alt="">
                                <div class="social">
                                </div>
                            </div>
                            <div class="member-info">
                                <h4>श्री अरविंद कुमार शर्मा</h4>
                                <span>माननीय नगर विकास मंत्री
                                (उत्तर प्रदेश)
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-6 d-flex align-items-stretch">
                        <div class="member aos-init aos-animate" data-aos="fade-up">
                            <div class="member-img">
                                <img src="Banner/IMG-StateMinister.jpg" class="img-fluid" alt="">
                                <div class="social">
                                </div>
                            </div>
                            <div class="member-info">
                                <h4>श्री राकेश राठौर गुरु</h4>
                                <span>माननीय नगर विकास राज्य मंत्री
                                (उत्तर प्रदेश)
                                </span>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-3 col-md-6 d-flex align-items-stretch">
                        <div class="member aos-init aos-animate" data-aos="fade-up" data-aos-delay="100">
                            <div class="member-img">
                                <img src="Banner/IMG-sachiv.jpg" class="img-fluid" alt="">
                                <div class="social">
                                </div>
                            </div>
                            <div class="member-info">
                                <h4>श्री अमृत अभिजात</h4>
                                <span>प्रमुख सचिव नगर विकास विभाग
                                (उत्तर प्रदेश)
                                </span>
                            </div>
                        </div>
                    </div>


                </div>

            </div>
        </section>
        <!-- ======= About Section ======= -->
        <section id="about" class="about">
            <div class="container">

                <div class="section-title" data-aos="zoom-out">
                    <h2>About</h2>
                    <p>नगर विकास विभाग: एक परिचय</p>
                </div>

                <div class="row content" data-aos="fade-up">
                    <div class="col-lg-6">
                        <p>
                            सभ्यता के विकास के साथ शहरीकरण की प्रवृत्ति भी बढ़ी है। शहरी क्षेत्रों में अवस्थापना तथा मूलभूत नागरिक सुविधाओं पर अपेक्षाकृत अधिक दबाव होने के कारण कालान्तर से ही शहरों के सुनियोजित विकास की आवश्यकता का अनुभव किया जाता रहा है। इसी क्रम में प्रदेश के नगरीय क्षेत्रों को एक स्वतंत्र इकाई, नगरीय स्थानीय निकाय, के रूप में अंगीकार किया गया है। उत्तर प्रदेश भारत वर्ष में सर्वाधिक जनसंख्या के साथ ही सर्वाधिक नगरीय स्थानीय निकायों वाला प्रदेश है। वर्तमान में उत्‍तर प्रदेश में कुल 762 नगरीय स्थानीय निकायें हैं, जिनमें 17 नगर निगम, 200 नगर पालिका परिषद एवं 545 नगर पंचायत हैं। प्रदेश की लगभग 22 प्रतिशत से अधिक आबादी इन नगरीय स्थानीय निकायों में निवास करती है। नगरीय स्थानीय निकायों के क्षेत्र में निवास करने वाली जनसंख्या को मूलभूत नागरिक सुविधाएं यथा- स्वच्छ पेयजलापूर्ति, सड़कें/गलिया, जल निकासी, सफाई व्यवस्था, कूड़ा निस्तारण, सीवरेज व्यवस्था, मार्ग प्रकाश, पार्क, स्वच्छ पर्यावरण, आदि उपलब्ध कराया जाना, इन नागर स्थानीय निकायों का कर्तव्य एवं उत्तरदायित्व है। वर्तमान में इस विभाग के अधीन निम्न संगठन/संस्थान कार्यरत हैं -
                        </p>
                        <ul>
                            <li><i class="ri-check-double-line"></i>स्थानीय निकाय निदेशालय.</li>
                            <li><i class="ri-check-double-line"></i>उत्तर प्रदेश जल निगम</li>
                            <li><i class="ri-check-double-line"></i>निर्माण एवं परिकल्प सेवाएं (कन्स्ट्रक्शन एण्ड डिजाइन सर्विसेज-सी.एण्ड डी.एस)</li>
                            <li><i class="ri-check-double-line"></i>क्षेत्रीय नागर एवं पर्यावरण अध्ययन केन्द्र</li>
                            <li><i class="ri-check-double-line"></i>उत्तर प्रदेश राज्य गंगा नदी संरक्षण प्राधिकरण</li>
                            <li><i class="ri-check-double-line"></i>जल संस्थान/जलकल</li>
                            <li><i class="ri-check-double-line"></i>नगरीय स्थानीय निकाय</li>
                            <li><i class="ri-check-double-line"></i>उत्तर प्रदेश नगर पालिका वित्तीय संसाधन विकास बोर्ड</li>


                        </ul>
                    </div>
                    <div class="col-lg-6 pt-4 pt-lg-0">
                        <p>
                            प्रदेश की नागर स्थानीय निकायों के कार्यों पर प्रशासकीय नियंत्रण के साथ-साथ नगरीय क्षेत्रों में अवस्थापना सुविधाओं के विकास एवं विस्तार हेतु विभिन्न योजनाओं/कार्यक्रमों के माध्यम से आवश्यक वित्तीय सहायता उपलब्ध कराये जाने हेतु नगर विकास विभाग का गठन किया गया। नगर विकास विभाग द्वारा उक्त कार्यों के अतिरिक्त शहरों में सेनिटेशन, पर्यावरण संरक्षण तथा नदियों/झीलों में प्रदूषण नियंत्रण आदि का कार्य भी किया जा रहा है। स्वतंत्रता के पूर्व राज्य सरकार स्तर पर इस विभाग का नाम लोक स्वास्थ्य विभाग था, जिसे बाद में स्थानीय स्वायत्त शासन विभाग नाम दिया गया। कालान्तर में इसे आवास एवं नगर विकास विभाग कहा गया। बाद में इस विभाग को दो अलग-अलग विभागों यथा आवास एवं शहरी नियोजन विभाग तथा नगर विकास विभाग में विभाजित कर दिया गया। शासन स्तर पर नगर विकास विभाग का कार्यालय बापू भवन में अवस्थित है। कार्यों के सम्पादन हेतु विभाग को 09 अनुभागों में बांटा गया है। इसके अतिरिक्त गंगा सेल व लेखा अनुभाग भी है। .
                        </p>
                        <a href="#" class="btn-learn-more">Learn More</a>
                    </div>
                </div>

            </div>
        </section>
        <!-- End About Section -->
        <!-- ======= Services Section ======= -->
        <section id="services" class="services">
            <div class="container">

                <div class="section-title" data-aos="zoom-out">
                    <h2>Schemes</h2>
                    <p></p>
                </div>

                <div class="row">
                    <div class="col-lg-4 col-md-6">
                        <div class="icon-box" data-aos="zoom-in-left">
                            <div class="icon"><i class="las la-basketball-ball" style="color: #ff689b;"></i></div>
                            <h4 class="title"><a href="">कान्हा गौशाला एवं बेसहारा पशु आश्रय योजना</a></h4>
                            <p class="description">देशभर में विभिन्न प्रकार की गौशाला स्थित है। इन सभी गौशालाओं के विकास के लिए सरकार निरंतर प्रयास करती है। जिसके लिए सरकार द्वारा विभिन्न प्रकार की योजनाओं का संचालन किया जाता है। इन योजनाओं के माध्यम से सरकार द्वारा गौशालाओं को आर्थिक सहायता प्रदान की जाती है। .</p>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6 mt-5 mt-md-0">
                        <div class="icon-box" data-aos="zoom-in-left" data-aos-delay="100">
                            <div class="icon"><i class="las la-book" style="color: #e9bf06;"></i></div>
                            <h4 class="title"><a href="">पेय जल हेतु व्यवस्था</a></h4>
                            <p class="description">भूमिका सुशासन के कार्यक्रम अन्तर्गत राज्य सरकार के सात निश्चय के तहत हर घर नल का जल के क्रियान्वयन हेतु मुख्यमंत्री ग्रामीण पेयजल निश्चय योजना वर्ष 2016-17 से प्रारंभ की गई है।.</p>
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-6 mt-5 mt-lg-0 ">
                        <div class="icon-box" data-aos="zoom-in-left" data-aos-delay="200">
                            <div class="icon"><i class="las la-file-alt" style="color: #3fcdc7;"></i></div>
                            <h4 class="title"><a href="">सीवरेज एवं जल निकासी हेतु व्यवस्था</a></h4>
                            <p class="description">विभाग ने बाढ़ को रोकने के लिए पहाड़ी ढलानों से नीचे बहते पानी को रोकने और इसे शहरी क्षेत्रों से मोड़ने के लिए डिज़ाइन की गई कई महत्वपूर्ण ऊबड़-खाबड़ सुरंगों का निर्माण किया है।.</p>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6 mt-5">
                        <div class="icon-box" data-aos="zoom-in-left" data-aos-delay="300">
                            <div class="icon"><i class="las la-tachometer-alt" style="color: #41cf2e;"></i></div>
                            <h4 class="title"><a href="">पं. दीनदयाल उपाध्याय आदर्श नगर पंचायत योजना</a></h4>
                            <p class="description">दीन दयाल उपाध्याय आदर्श नगर पंचायत योजना के नाम से योजना प्रारम्भ की गई है। योजना के तहत वित्तीय वर्ष 2020-21 के लिए 26 जनपदों की नगर निकायों का चयन कर उन्हें आदर्श नगर पंचायत बनाए जाने का निर्णय लिया गया है। राज्य के अन्य जिलों से भी प्रस्ताव आ गए हैं, जल्द ही अन्य जिलों की नगर पंचायतों का चयन भी कर लिया जाएगा।.</p>
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-6 mt-5">
                        <div class="icon-box" data-aos="zoom-in-left" data-aos-delay="400">
                            <div class="icon"><i class="las la-globe-americas" style="color: #d6ff22;"></i></div>
                            <h4 class="title"><a href="">मुख्यमंत्री नगरीय अल्प विकसित व मलिन बस्ती विकास योजना</a></h4>
                            <p class="description">अतः अल्पसंख्यक बाहुल्य बस्तियों तथा मलिन बस्तियों में "सी०सी० रोड अथवा इण्टरलाकिंग, नाली निर्माण व अन्य सामान्य सुविधाओं की स्थापना योजना" के अवशेष कार्यों को पूर्ण कराने हेतु "मुख्यमंत्री नगरीय अल्पविकसित व मलिन बस्ती विकास योजना" में योजनावार निर्णय शासन द्वारा लिया जायेगा।.</p>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6 mt-5">
                        <div class="icon-box" data-aos="zoom-in-left" data-aos-delay="500">
                            <div class="icon"><i class="las la-clock" style="color: #4680ff;"></i></div>
                            <h4 class="title"><a href="">झील संरक्षण कार्यक्रम योजना</a></h4>
                            <p class="description">राष्ट्रीय झील संरक्षण योजना (NLCP): पर्यावरण और वन मंत्रालय शहरी और अर्ध-शहरी क्षेत्रों में प्रदूषित और अवक्रमित झीलों के संरक्षण और प्रबंधन के लिए 2001 से राष्ट्रीय झील संरक्षण योजना (एनएलसीपी) लागू कर रहा है।</p>
                        </div>
                    </div>

                    <div class="col-lg-4 col-md-6 mt-5">
                        <div class="icon-box" data-aos="zoom-in-left" data-aos-delay="500">
                            <div class="icon"><i class="las la-universal-access" style="color: #d6ff22;"></i></div>
                            <h4 class="title"><a href="">नगरीय पेयजल योजना (एक लाख से कम आबादी)</a></h4>
                            <p class="description">योगी सरकार ने देश के 77वें स्वतंत्रता दिवस पर राज्य की नौ करोड़ ग्रामीण आबादी को शुद्ध पेयजल का अब तक का सबसे बड़ा तोहफा दिया। प्रधानमंत्री नरेन्द्र मोदी की महत्वाकांक्षी योजना जल जीवन मिशन से 15 अगस्त को राज्य के 1,50,27,692 ग्रामीण परिवारों को स्वच्छ पेयजल मिलने लगा। </p>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6 mt-5">
                        <div class="icon-box" data-aos="zoom-in-left" data-aos-delay="500">
                            <div class="icon"><i class="las la-horse-head" style="color: #808080;"></i></div>
                            <h4 class="title"><a href="">नगरीय झील तालाब पोखर संरक्षण योजना</a></h4>
                            <p class="description">
                                प्रदेश सरकार ने तालाब, पोखरों व झीलों के संरक्षण का फरमान जारी किया है। इसके लिए जनपद स्तरीय अधिकारियों से शहर के तालाब, पोखरों व झीलों के संरक्षण के लिए प्रस्ताव मांगे गए हैं। आदेश में कहा गया है कि संरक्षण उन जलस्त्रोतों का किया जाएगा जो विवादित न हों। ताल पोखरों व झीलों को संरक्षित करने वाली कार्यदायी संस्था जहां सुंदरीकरण कराएगी वहीं पांच वर्ष तक इनके संरक्षण की भी जिम्मेदारी होगी।

                            </p>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6 mt-5">
                        <div class="icon-box" data-aos="zoom-in-left" data-aos-delay="500">
                            <div class="icon"><i class="las la-radiation" style="color: #4680ff;"></i></div>
                            <h4 class="title"><a href="">पंडित दीन दयाल उपाधयाय नगर विकास योजना (अनुदान 37)</a></h4>
                            <p class="description">योजना का उद्देश्य कौशल विकास और अन्य उपायों के माध्यम से आजीविका के अवसरों में वृद्धि कर शहरी और ग्रामीण गरीबी को कम करना है। मेक इन इंडिया, कार्यक्रम के उद्देश्य को ध्यान में रखते हुए सामाजिक तथा आर्थिक बेहतरी के लिए कौशल विकास आवश्यक है।</p>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6 mt-5">
                        <div class="icon-box" data-aos="zoom-in-left" data-aos-delay="500">
                            <div class="icon"><i class="las la-frog" style="color: #4680ff;"></i></div>
                            <h4 class="title"><a href="">डॉ० ए०पी०जे० अब्दुल कलाम नगरीय सौर पुंज योजना</a></h4>
                            <p class="description">सरकार ने डॉ। एपीजे अब्दुल कलाम शहरी सौर पुंज योजना के तहत 1.91 करोड़ की राशि जारी की है। इस पैसे से शहर की सड़कों को रोशन करने के लिए सौर ऊर्जा संयंत्र और सौर स्ट्रीट लाइटें लगाई जाएंग </p>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6 mt-5">
                        <div class="icon-box" data-aos="zoom-in-left" data-aos-delay="500">
                            <div class="icon"><i class="las la-sort-amount-up" style="color: #ff689b;"></i></div>
                            <h4 class="title"><a href="">स्मार्ट सिटी मिशन कार्यक्रम</a></h4>
                            <p class="description">स्मार्ट सिटी मिशन कार्यक्रम - एकमुश्त- बरेली नगर हेतु भारत सरकार से प्रोजेक्ट फण्ड एवं ए0एण्डओ0ई0 मद में प्राप्त केन्द्रांश एवं संगत राज्यांश कुल रू0 49.00 करोड़ की स्वीकृति।</p>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6 mt-5">
                        <div class="icon-box" data-aos="zoom-in-left" data-aos-delay="500">
                            <div class="icon"><i class="las la-upload " style="color: #4680ff;"></i></div>
                            <h4 class="title"><a href="">शहरी क्षत्रों में अंत्योष्टि स्थल विकास योजना</a></h4>
                            <p class="description">प्रदेश में बड़े शहरों की ओर शहरीकरण काफी तेजी से विकास कर रहा है। परिणामस्वरूप, देश के विभिन्न राज्यों से लोग उत्तर प्रदेश में पलायन कर रहे हैं, और रोजगार के अवसरों का फायदा उठा रहे हैं। प्रदेश में शहरीकरण एक उचित दिशा में अग्रसर है, जिससे काफी तेजी से विकास हो रहा है, आर्थिक क्षमताओं और रोजगार कमाने के और भी अवसर प्रदान करता है। इसलिए, शहरीकरण के चलते ग्रामीण क्षेत्रों की अपेक्षा शहरों में अधिक रोजगार बढ़ रहा है, साथ ही इससे शहरों का आर्थिक सुधार भी संभव हो सकता है।</p>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6 mt-5">
                        <div class="icon-box" data-aos="zoom-in-left" data-aos-delay="500">
                            <div class="icon"><i class="las la-kiwi-bird" style="color: #41cf2e;"></i></div>
                            <h4 class="title"><a href="">नगरीय पेयजल कार्यक्रम (जिला योजना)</a></h4>
                            <p class="description">नगरीय पेयजल कार्यक्रम (जिला सेक्‍टर-सामान्‍य)- पेयजलापूर्ति कार्यों हेतु रू0 2500.00 लाख की स्वीकृति। नागर स्थानीय निकायों को प्रोत्साहन हेतु 2% प्रतिशत की धनराशि की स्वीकृति। नागर स्थानीय निकायों को प्रोत्साहन हेतु 2% प्रतिशत की धनराशि की स्वीकृति।</p>
                        </div>
                    </div>

                </div>

            </div>
        </section>
        <!-- End Services Section -->
        <!-- ======= Portfolio Section ======= -->
        <section id="portfolio" class="portfolio">
            <div class="container">

                <div class="section-title" data-aos="zoom-out">
                    <h2>Portfolio</h2>
                    <p>What we've done</p>
                </div>
                <div id="chartContainer" style="height: 370px; width: 100%;"></div>

            </div>

        </section>

        <!-- End Portfolio Section -->
        <!-- ======= Testimonials Section ======= -->
        <section id="testimonials" class="testimonials">
            <div class="container">

                <div class="section-title" data-aos="zoom-out">
                    <h2>Major</h2>
                    <p>Schemes</p>
                </div>

                <div class="owl-carousel testimonials-carousel" data-aos="fade-up">

                    <div class="testimonial-item">
                        <p>
                            <i class="bx bxs-quote-alt-left quote-icon-left"></i>
                            <h3>कान्हा गौशाला एवं बेसहारा पशु आश्रय योजना</h3>
                            <i class="bx bxs-quote-alt-right quote-icon-right"></i>
                        </p>
                        <img src="SSAicon/Funds%20Pending%20for%20transfer%20to%20State%20Implementing%20Society%20by%20State%20Govt..png" class="testimonial-img" alt="">
                    </div>

                    <div class="testimonial-item">
                        <p>
                            <i class="bx bxs-quote-alt-left quote-icon-left"></i>
                            <h3>नगरीय पेयजल कार्यक्रम (जिला योजना)</h3>
                            <i class="bx bxs-quote-alt-right quote-icon-right"></i>
                        </p>
                        <img src="SSAicon/CompositeSchoolGrant.png" class="testimonial-img" alt="">
                    </div>

                    <div class="testimonial-item">
                        <p>
                            <i class="bx bxs-quote-alt-left quote-icon-left"></i>
                            <h3>शहरी क्षत्रों में अंत्योष्टि स्थल विकास योजना</h3>
                            <i class="bx bxs-quote-alt-right quote-icon-right"></i>
                        </p>
                        <img src="SSAicon/ict%20in%20school%20education.png" class="testimonial-img" alt="">
                    </div>

                    <div class="testimonial-item">
                        <p>
                            <i class="bx bxs-quote-alt-left quote-icon-left"></i>
                            <h3>स्मार्ट सिटी मिशन कार्यक्रम</h3>
                            <i class="bx bxs-quote-alt-right quote-icon-right"></i>
                        </p>
                        <img src="SSAicon/KGBV.png" class="testimonial-img" alt="">
                    </div>

                    <div class="testimonial-item">
                        <p>
                            <i class="bx bxs-quote-alt-left quote-icon-left"></i>
                            <h3>डॉ० ए०पी०जे० अब्दुल कलाम नगरीय सौर पुंज योजना</h3>
                            <i class="bx bxs-quote-alt-right quote-icon-right"></i>
                        </p>
                        <img src="SSAicon/Library%20Grant.png" class="testimonial-img" alt="">
                    </div>

                    <div class="testimonial-item">
                        <p>
                            <i class="bx bxs-quote-alt-left quote-icon-left"></i>
                            <h3>पंडित दीन दयाल उपाधयाय नगर विकास योजना (अनुदान 37)</h3>
                            <i class="bx bxs-quote-alt-right quote-icon-right"></i>
                        </p>
                        <img src="SSAicon/r.png" class="testimonial-img" alt="">
                    </div>

                    <div class="testimonial-item">
                        <p>
                            <i class="bx bxs-quote-alt-left quote-icon-left"></i>
                            <h3>नगरीय झील तालाब पोखर संरक्षण योजना</h3>
                            <i class="bx bxs-quote-alt-right quote-icon-right"></i>
                        </p>
                        <img src="SSAicon/release%20&%20Expenditure.png" class="testimonial-img" alt="">
                    </div>

                    <div class="testimonial-item">
                        <p>
                            <i class="bx bxs-quote-alt-left quote-icon-left"></i>
                            <h3>नगरीय पेयजल योजना (एक लाख से कम आबादी)</h3>
                            <i class="bx bxs-quote-alt-right quote-icon-right"></i>
                        </p>
                        <img src="SSAicon/self-defence%20training%20to%20girls.png" class="testimonial-img" alt="">
                    </div>

                    <div class="testimonial-item">
                        <p>
                            <i class="bx bxs-quote-alt-left quote-icon-left"></i>
                            <h3>झील संरक्षण कार्यक्रम योजना</h3>
                            <i class="bx bxs-quote-alt-right quote-icon-right"></i>
                        </p>
                        <img src="SSAicon/smart-classroom_digital%20boards.png" class="testimonial-img" alt="">
                    </div>

                    <div class="testimonial-item">
                        <p>
                            <i class="bx bxs-quote-alt-left quote-icon-left"></i>
                            <h3>मुख्यमंत्री नगरीय अल्प विकसित व मलिन बस्ती विकास योजना</h3>
                            <i class="bx bxs-quote-alt-right quote-icon-right"></i>
                        </p>
                        <img src="SSAicon/Special%20Tranining%20of%20OoSCs.jpg" class="testimonial-img" alt="">
                    </div>

                    <div class="testimonial-item">
                        <p>
                            <i class="bx bxs-quote-alt-left quote-icon-left"></i>
                            <h3>पेय जल हेतु व्यवस्था</h3>
                            <i class="bx bxs-quote-alt-right quote-icon-right"></i>
                        </p>
                        <img src="SSAicon/sports%20&%20Physical%20education.png" class="testimonial-img" alt="">
                    </div>

                    <div class="testimonial-item">
                        <p>
                            <i class="bx bxs-quote-alt-left quote-icon-left"></i>
                            <h3>सीवरेज एवं जल निकासी हेतु व्यवस्था</h3>
                            <i class="bx bxs-quote-alt-right quote-icon-right"></i>
                        </p>
                        <img src="SSAicon/Stipends%20to%20CWSN%20Girls.png" class="testimonial-img" alt="">
                    </div>

                </div>

            </div>
        </section>
    </main>
    <!-- End #main -->

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {

            var _Data;

            var _lst = [];
            CanvasJS.addColorSet("greenShades",
                [//colorSet Array

                    "#ed3709",
                    "#e8ab1c",
                    "#ebe30e",
                    "#1dddf2",
                    "#90EE90"
                ]);


            var chart = new CanvasJS.Chart("chart1Container", {
                theme: "light2", // "light1", "light2", "dark1", "dark2"
                exportEnabled: true,
                animationEnabled: true,
                colorSet: "greenShades",
                title: {
                    text: "Component Wise Amount",
                    fontSize: 20,
                    fontWeight: "normal"
                },
                data: [{
                    type: "pie",
                    startAngle: 300,
                    toolTipContent: "<b>{label}</b>: {y}",
                    //showInLegend: "true",
                    //legendText: "{label}",
                    indexLabelFontSize: 13,
                    //indexLabel: "{label} - {y}",
                    dataPoints: _lst
                }]
            });
            chart.render();

        })

        var Multiline = new CanvasJS.Chart("MultilineContainer", {
            theme: "light2",
            animationEnabled: true,
            title: {
                text: ""
            },
            axisY: {
                includeZero: false,
                title: "Fund Unspent %(Month wise)",
                fontSize: 20,
                fontWeight: "normal",
                suffix: "cr"
            },
            toolTip: {
                shared: "true"
            },
            legend: {
                cursor: "pointer",
                itemclick: toggleDataSeries
            },
            data: [{
                type: "spline",
                visible: false,
                showInLegend: true,
                yValueFormatString: "##.00cr",
                name: "Salary",
                dataPoints: [
                    { label: "Apr", y: 2.22 },
                    { label: "May", y: 2.20 },
                    { label: "Jun", y: 2.44 },
                    { label: "Jul", y: 2.45 },
                    { label: "Aug", y: 2.58 },
                    { label: "Sep", y: 2.44 },
                    { label: "Oct", y: 2.40 },
                    { label: "Nov", y: 2.72 },
                    { label: "Dec", y: 2.72 },
                    { label: "Jan", y: 2.72 },
                    { label: "Feb", y: 2.66 },
                    { label: "Mar", y: 3.04 }
                ]
            },
            {
                type: "spline",
                showInLegend: true,
                visible: false,
                yValueFormatString: "##.00cr",
                name: " Hiring, Repair & Maintenance of Vechile/POL",
                dataPoints: [
                    { label: "Apr", y: 3.86 },
                    { label: "May", y: 3.76 },
                    { label: "Jun", y: 3.77 },
                    { label: "Jul", y: 3.65 },
                    { label: "Aug", y: 3.90 },
                    { label: "Sep", y: 3.88 },
                    { label: "Oct", y: 3.69 },
                    { label: "Nov", y: 3.86 },
                    { label: "Dec", y: 3.38 },
                    { label: "Jan", y: 4.20 },
                    { label: "Feb", y: 6.30 },
                    { label: "Mar", y: 2.20 },
                ]
            },
            {
                type: "spline",
                visible: false,
                showInLegend: true,
                yValueFormatString: "##.00cr",
                name: " Consultancy Charges including audit fees",
                dataPoints: [
                    { label: "Apr", y: 4.37 },
                    { label: "May", y: 4.27 },
                    { label: "Jun", y: 4.72 },
                    { label: "Jul", y: 4.87 },
                    { label: "Aug", y: 5.35 },
                    { label: "Sep", y: 5.50 },
                    { label: "Oct", y: 4.84 },
                    { label: "Nov", y: 4.13 },
                    { label: "Dec", y: 5.22 },
                    { label: "Jan", y: 2.22 },
                    { label: "Feb", y: 5.22 },
                    { label: "Mar", y: 5.39 }
                ]
            },
            {
                type: "spline",
                showInLegend: true,
                yValueFormatString: "##.00cr",
                name: " Telephone Expenses and Internet",
                dataPoints: [
                    { label: "Apr", y: 6.64 },
                    { label: "May", y: 6.31 },
                    { label: "Jun", y: 6.59 },
                    { label: "Jul", y: 6.95 },
                    { label: "Aug", y: 7.16 },
                    { label: "Sep", y: 6.40 },
                    { label: "Oct", y: 7.20 },
                    { label: "Nov", y: 7.17 },
                    { label: "Dec", y: 6.95 },
                    { label: "Jan", y: 5.95 },
                    { label: "Feb", y: 6.95 },
                    { label: "Mar", y: 7.09 }
                ]
            },
            {
                type: "spline",
                showInLegend: true,
                yValueFormatString: "##.00cr",
                name: "EMIS",
                dataPoints: [
                    { label: "Apr", y: 8 },
                    { label: "May", y: 6.81 },
                    { label: "Jun", y: 6.71 },
                    { label: "Jul", y: 6.82 },
                    { label: "Aug", y: 6.56 },
                    { label: "Sep", y: 6.24 },
                    { label: "Oct", y: 5.40 },
                    { label: "Nov", y: 7.01 },
                    { label: "Dec", y: 7.14 },
                    { label: "Jan", y: 8.11 },
                    { label: "Feb", y: 7.11 },
                    { label: "Mar", y: 8.11 }
                ]
            },
            {
                type: "spline",
                showInLegend: true,
                yValueFormatString: "##.00cr",
                name: "Upper Primary Teachers (Contractual i.e. PTI)",
                dataPoints: [
                    { label: "Apr", y: 7.94 },
                    { label: "May", y: 7.29 },
                    { label: "Jun", y: 7.28 },
                    { label: "Jul", y: 7.82 },
                    { label: "Aug", y: 7.89 },
                    { label: "Sep", y: 6.71 },
                    { label: "Oct", y: 7.80 },
                    { label: "Nov", y: 7.60 },
                    { label: "Dec", y: 7.66 },
                    { label: "Jan", y: 6.96 },
                    { label: "Feb", y: 7.26 },
                    { label: "Mar", y: 8.89 }
                ]
            },
            {
                type: "spline",
                showInLegend: true,
                yValueFormatString: "##.00cr",
                name: " Salary (Previous Spl. Educators)",
                dataPoints: [
                    { label: "Apr", y: 7.94 },
                    { label: "May", y: 7.29 },
                    { label: "Jun", y: 7.28 },
                    { label: "Jul", y: 7.82 },
                    { label: "Aug", y: 7.89 },
                    { label: "Sep", y: 6.71 },
                    { label: "Oct", y: 7.80 },
                    { label: "Nov", y: 7.60 },
                    { label: "Dec", y: 7.66 },
                    { label: "Jan", y: 2.66 },
                    { label: "Feb", y: 7.66 },
                    { label: "Mar", y: 8.89 }
                ]
            },
            {
                type: "spline",
                showInLegend: true,
                yValueFormatString: "##.00cr",
                name: "Salary for 2 Resource Person for CWSN",
                dataPoints: [
                    { label: "Apr", y: 7.94 },
                    { label: "May", y: 7.29 },
                    { label: "Jun", y: 7.28 },
                    { label: "Jul", y: 7.82 },
                    { label: "Aug", y: 7.89 },
                    { label: "Sep", y: 6.71 },
                    { label: "Oct", y: 7.80 },
                    { label: "Nov", y: 7.60 },
                    { label: "Dec", y: 2.60 },
                    { label: "Jan", y: 7.60 },
                    { label: "Feb", y: 6.66 },
                    { label: "Mar", y: 8.89 }
                ]
            }]
        });
        Multiline.render();

        function toggleDataSeries(e) {
            if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                e.dataSeries.visible = false;
            } else {
                e.dataSeries.visible = true;
            }
            Multiline.render();
        }

    </script>
</asp:Content>

