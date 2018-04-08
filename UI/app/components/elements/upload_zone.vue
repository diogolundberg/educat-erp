<template>
  <div>
    <input
      ref="file"
      type="file"
      style="position: fixed; top: -100em"
      @change="pick">
    <div
      class="pointer"
      @click="!disabled && $refs.file.click()">
      <slot />
    </div>
  </div>
</template>

<script>
  import axios from "axios";

  export default {
    name: "UploadZone",
    props: {
      value: {
        type: String,
        required: false,
        default: null,
      },
      prefix: {
        type: [String, Number],
        required: false,
        default: null,
      },
      acceptedTypes: {
        type: Array,
        default: () => ["application/pdf", "image/jpeg", "image/png"],
      },
      disabled: {
        type: Boolean,
        default: false,
      },
    },
    methods: {
      async pick() {
        const file = this.$refs.file.files[0];
        if (!this.acceptedTypes.includes(file.type)) {
          this.notify("Tipo de arquivo não aceito.");
          return;
        }

        await this.$store.dispatch("presign", `${this.prefix}${file.name}`);
        const url = this.$store.getters.uploadUrl;

        const headers = {
          "x-ms-blob-type": "BlockBlob",
          "x-ms-blob-content-type": file.type,
        };
        await axios.put(url, file, { headers });
        this.$emit("input", url.split("?")[0]);
      },
    },
  };
</script>
