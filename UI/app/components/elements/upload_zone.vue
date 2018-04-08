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
      disabled: {
        type: Boolean,
        default: false,
      },
    },
    methods: {
      async pick() {
        this.loading = true;
        this.loaded = 0;

        const file = this.$refs.file.files[0];
        await this.$store.dispatch("presign", `${this.prefix}${file.name}`);
        const url = this.$store.getters.uploadUrl;

        const headers = { "x-ms-blob-type": "BlockBlob" };
        await axios.put(url, file, { headers, onUploadProgress: this.setProg });
        this.$emit("input", url.split("?")[0]);
        this.loading = false;
      },
      setProg(e) {
        const ratio = e.loaded / e.total;
        this.loaded = Math.floor(ratio * 100.0);
      },
    },
  };
</script>
